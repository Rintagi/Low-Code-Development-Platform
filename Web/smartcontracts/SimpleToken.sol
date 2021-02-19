// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import { ERC20 } from "./ERC20.sol";
import { Ownable } from "./openzeppelin-solidity/ownership/Ownable.sol";
import { Pausable } from "./openzeppelin-solidity/lifecycle/Pausable.sol";
import "./openzeppelin-solidity/math/SafeMath.sol";

/**
* @dev for smart contract signature verification
*/
interface IERC1271 {
  // valid if return is 
  // bytes4(keccak256("isValidSignature(bytes32,bytes)")
  // bytes4 constant internal MAGICVALUE = 0x1626ba7e;
  // some oddball implementations
  // bytes4(keccak256("isValidSignature(bytes,bytes)")
  // bytes4 constant internal MAGICVALUE = 0x20c13b0b;

  function isValidSignature(
    bytes32 _messageHash,
    bytes memory _signature)
    external
    view
    returns (bytes4 magicValue);
}

struct EIP712Domain {
    string name;
    string version;
    uint256 chainId;
    address verifyingContract;
}
/**
* @dev simple ERC20 with gasless permit
*/
contract SimpleToken is ERC20, Ownable, Pausable {
    using SafeMath for uint256;

    /* metadata */
    string public constant name = "SimpleToken";
    string public constant symbol = "SimT";
    string public constant version = "1.0";
    uint8 public constant decimals = 18;

    uint256 public constant INITIAL_SUPPLY = 100000000 * 10**18;
    uint256 public constant MAX_INT = 2**256 - 1;
    string public domain_seperator_type = "EIP712Domain(string name,string version,uint256 chainId,address verifyingContract)";
    bytes32 public domain_seperator_hash;
    string public authorize_type = "authorize(address holder,address spender,uint256 nonce,uint256 expiry,uint256 value)";
    bytes32 public authorize_type_hash;
    string public permit_type = "permit(address holder,address spender,uint256 nonce,uint256 expiry,bool allowed)";
    bytes32 public permit_type_hash;
    EIP712Domain public eip712_domain;
    // gasless signing sequencer
    mapping (address => uint)                      public nonces;

    /**
    * @dev constructor 
    */
    constructor()
    {
        uint256 chainId = getChainId();
        require((chainId > 0), "must supply chainId for domain seperator");
        // --- EIP712 niceties ---
        authorize_type_hash = keccak256(bytes(authorize_type));
        permit_type_hash = keccak256(bytes(permit_type));
        domain_seperator_hash = keccak256(abi.encode(
            keccak256(bytes(domain_seperator_type)),
            keccak256(bytes(name)), keccak256(bytes(version)), chainId, address(this)));
        _mint(msg.sender, INITIAL_SUPPLY);
    }

    function getEIP712Domain() public view returns(EIP712Domain memory) {
        uint256 chainId = getChainId();
        EIP712Domain memory domain = EIP712Domain({
            name: name,
            version: version,
            chainId: chainId,
            verifyingContract: address(this)
        });
        return domain;
    }

    function authorize_verified(address holder, address spender, uint256 nonce, uint256 expiry, uint256 value, bytes calldata signature) public view returns (bool)
    {
        bool isContract = isValidContractAddress(holder);
        bytes32 digest = keccak256(abi.encodePacked(
            "\x19\x01",
            domain_seperator_hash,
            keccak256(abi.encode(authorize_type_hash,
                                holder,
                                spender,
                                nonce,
                                expiry,
                                value))));

        if (!isContract) {
            uint8 v;bytes32 r;bytes32 s;
            (v, r, s) = splitSignature(signature);
            return holder == ecrecover(digest, v, r, s);
        }
        else {
            IERC1271 smartWallet = IERC1271(holder);
            bytes4 magic = smartWallet.isValidSignature(digest, signature);
            return  magic == 0x1626ba7e || magic == 0x20c13b0b;
        }
    }

    // --- Approve by signature, this is NOT the same as in Dai ---
    function authorize(address holder, address spender, uint256 nonce, uint256 expiry, uint256 value, bytes calldata signature) public returns (bool)
    {
        require(holder != address(0), "simt/invalid holder");
        require(authorize_verified(holder, spender, nonce, expiry, value, signature), "simt/invalid-authorize");
        require(expiry == 0 || block.timestamp <= expiry, "simt/authorize-expired");
        require(nonce == nonces[holder]++, "simt/invalid-nonce"); // bad style side-effect in test but does save some gas

        _allowed[holder][spender] = value; // require change to base ERC20 class to internal for _allowed
        emit Approval(holder, spender, value);
        return true;
    }

    function permit_verified(address holder, address spender, uint256 nonce, uint256 expiry, bool allowed, uint8 v, bytes32 r, bytes32 s) public view returns (bool)
    {
        bool isContract = isValidContractAddress(holder);
        bytes32 digest = keccak256(abi.encodePacked(
            "\x19\x01",
            domain_seperator_hash,
            keccak256(abi.encode(permit_type_hash,
                                holder,
                                spender,
                                nonce,
                                expiry,
                                allowed))));

        if (!isContract) {
            return holder == ecrecover(digest, v, r, s);
        }
        else {
            IERC1271 smartWallet = IERC1271(holder);
            bytes memory signature = packSignature(v, r, s);
            bytes4 magic = smartWallet.isValidSignature(digest, signature);
            return  magic == 0x1626ba7e || magic == 0x20c13b0b;
        }
    }
    // --- Approve by signature, this is compatible to Dai ---
    function permit(address holder, address spender, uint256 nonce, uint256 expiry, bool allowed, uint8 v, bytes32 r, bytes32 s) public returns (bool)
    {
        require(holder != address(0), "simt/invalid holder");
        require(permit_verified(holder, spender, nonce, expiry, allowed, v, r, s), "simt/invalid-permit");
        require(expiry == 0 || block.timestamp <= expiry, "simt/authorize-expired");
        require(nonce == nonces[holder]++, "simt/invalid-nonce"); // bad style side-effect in test but does save some gas

        uint can = allowed ? MAX_INT : 0;
        _allowed[holder][spender] = can; // require change to base ERC20 class to internal for _allowed
        emit Approval(holder, spender, can);
        return true;
    }

    function getChainId() public view returns (uint256) {
        uint256 id;
        assembly {
            id := chainid()
        }
        return id;
    }

    function packSignature(uint8 v, bytes32 r, bytes32 s) public pure returns(bytes memory) {
        return abi.encodePacked(r,s,v);
    }

    function splitSignature(bytes memory sig) public pure returns (uint8, bytes32, bytes32)
    {
        require(sig.length == 65, "invalid signature length, must be 65 bytes");

        bytes32 r;
        bytes32 s;
        uint8 v;

        assembly {
            // first 32 bytes, after the length prefix
            r := mload(add(sig, 32))
            // second 32 bytes
            s := mload(add(sig, 64))
            // final byte (first byte of the next 32 bytes)
            v := byte(0, mload(add(sig, 96)))
        }
        if (v < 27) {
            v += 27;
        }
        require(v == 27 || v == 28, "invalid v in sig");
    
        return (v, r, s);
    }

    function isValidContractAddress(address _addr) public view returns (bool hasCode) {
        // test if an address is an on chain contract
        // do not use this for security check(i.e. must not be contract) as the following call seq can trick the check
        // Attacker Contract constructor -> Victim contract function -> isValidContractAddress(msg.sender) => false, i.e. code size 0
        assembly {
            // retrieve the size of the code, this needs assembly
            let size := extcodesize(_addr)
            hasCode := gt(size,0)
        }
    }
}