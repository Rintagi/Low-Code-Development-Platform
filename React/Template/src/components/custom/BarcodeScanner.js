import React, { useCallback, useLayoutEffect } from 'react';
import PropTypes from 'prop-types';
import Quagga from '@ericblade/quagga2';
import jsQR from 'jsqr';
//import QrCodeReader from './QrCodeReader';

class QrCodeReader {
    // TODO: is FORMAT, _row, config, supplements actually necessary? check inside quagga to see if
    // they are used for anything? or if they are just customary.
    // FORMAT: {
    //     value: 'qr_code',
    //     writeable: false,
    // };

    // _row: [];

    // config: {};

    // supplements: any;

    constructor(config, supplements) {
        this._row = [];
        this.config = config || {};
        this.supplements = supplements;
        this.FORMAT = {
            value: 'qr_code',
            writeable: false,
        };
        return this;
    }

    decodeImage(inputImageWrapper) {
        const data = inputImageWrapper.getAsRGBA();
        const result = jsQR(data, inputImageWrapper.size.x, inputImageWrapper.size.y);
        if (result === null) {
            return null;
        }
        // TODO: translate result.location into same values as box/boxes from other readers?
        return {
            codeResult: {
                code: result.data,
                format: this.FORMAT.value,
            },
            ...result,
        };
    }

    decodePattern(pattern) {
        // STUB, this is probably meaningless to QR, but needs to be implemented for Quagga, in case
        // it thinks there's a potential barcode in the image
        return null;
    }
}

Quagga.registerReader('qrcode', QrCodeReader);

function getMedian(arr) {
    arr.sort((a, b) => a - b);
    const half = Math.floor(arr.length / 2);
    if (arr.length % 2 === 1) {
        return arr[half];
    }
    return (arr[half - 1] + arr[half]) / 2;
}

function getMedianOfCodeErrors(decodedCodes) {
    if (!decodedCodes) return 1;
    const errors = decodedCodes.filter(x => x.error !== undefined).map(x => x.error);
    const medianOfErrors = getMedian(errors);
    return medianOfErrors;
}

const defaultConstraints = {
    width: 640,
    height: 480,
};

const defaultLocatorSettings = {
    patchSize: 'medium',
    halfSample: true,
};

//const defaultDecoders = ['ean_reader', 'code_128_reader', 'code_39_reader','code_93_reader', 'qrcode', 'codabar_reader', 'code_32_reader'];
const defaultDecoders = ['qrcode'];

const BarcodeScanner = ({
    onDetected,
    scannerRef,
    onScannerReady,
    cameraId,
    facingMode,
    constraints = defaultConstraints,
    locator = defaultLocatorSettings,
    numOfWorkers = navigator.hardwareConcurrency || 0,
    decoders = defaultDecoders,
    locate = true,
    torch = false,
    zoom = 1.0,
    onError,
}) => {
    const errorCheck = useCallback((result) => {
//        console.log(result);
        if (!onDetected) {
            return;
        }
        const err = getMedianOfCodeErrors(result.codeResult.decodedCodes);
        // if Quagga is at least 75% certain that it read correctly, then accept the code.
        if (err < 0.15 ||
            (result.codeResult.code && result.codeResult.format === "qr_code")
        ) {
            onDetected(result.codeResult.code, result.codeResult.format);
        }
    }, [onDetected]);

    const handleProcessed = (result) => {
        return;
        const drawingCtx = Quagga.canvas.ctx.overlay;
        const drawingCanvas = Quagga.canvas.dom.overlay;
        drawingCtx.font = "24px Arial";
        drawingCtx.fillStyle = 'green';

        if (result) {
            // console.warn('* quagga onProcessed', result);
            if (result.boxes) {
                drawingCtx.clearRect(0, 0, parseInt(drawingCanvas.getAttribute('width'), 10), parseInt(drawingCanvas.getAttribute('height'), 10));
                result.boxes.filter((box) => box !== result.box).forEach((box) => {
                    Quagga.ImageDebug.drawPath(box, { x: 0, y: 1 }, drawingCtx, { color: 'purple', lineWidth: 2 });
                });
            }
            if (result.box) {
                Quagga.ImageDebug.drawPath(result.box, { x: 0, y: 1 }, drawingCtx, { color: 'blue', lineWidth: 2 });
            }
            if (result.codeResult && result.codeResult.code) {
                // const validated = barcodeValidator(result.codeResult.code);
                // const validated = validateBarcode(result.codeResult.code);
                // Quagga.ImageDebug.drawPath(result.line, { x: 'x', y: 'y' }, drawingCtx, { color: validated ? 'green' : 'red', lineWidth: 3 });
                drawingCtx.font = "24px Arial";
                // drawingCtx.fillStyle = validated ? 'green' : 'red';
                // drawingCtx.fillText(`${result.codeResult.code} valid: ${validated}`, 10, 50);
                drawingCtx.fillText(result.codeResult.code, 10, 20);
                // if (validated) {
                //     onDetected(result);
                // }
            }
        }
    };

    useLayoutEffect(() => {
        //navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia || navigator.mediaDevices.getUserMedia;
        //console.log(Navigator.getUserMedia);
        //console.log(Navigator);
        Quagga.init({
            inputStream: {
                type: 'LiveStream',
                constraints: {
                    ...constraints,
                    ...(cameraId && { deviceId: cameraId }),
                    ...(!cameraId && facingMode && { facingMode: facingMode }),
                },
                target: scannerRef.current,
                // area: { // defines rectangle of the detection/localization area
                //     top: "0%",    // top offset
                //     right: "0%",  // right offset
                //     left: "0%",   // left offset
                //     bottom: "0%"  // bottom offset
                //   },
            },
            locator,
            numOfWorkers,
            decoder: { readers: decoders },
            locate,
        }, (err) => {
            Quagga.onProcessed(handleProcessed);

            if (err) {
                if (onError) {
                    onError(err);
                }                
                return console.log('Error starting Quagga:', err);
            }
            const track = Quagga.CameraAccess.getActiveTrack();
            var capabilities = {};
            if (typeof track.getCapabilities === 'function') {
                capabilities = track.getCapabilities();
                console.log(capabilities);
                console.log(capabilities.zoom);
                console.log(capabilities.torch);
            }
            if (capabilities.zoom) track.applyConstraints({advanced: [{zoom: parseFloat(zoom)}]});            
            if (capabilities.torch) track.applyConstraints({advanced: [{torch: !!torch}]});

            if (scannerRef && scannerRef.current) {
                Quagga.start();
                if (onScannerReady) {
                    onScannerReady();
                }
            }
        });
        Quagga.onDetected(errorCheck);
        return () => {
            Quagga.offDetected(errorCheck);
            Quagga.offProcessed(handleProcessed);
            Quagga.stop();
        };
    }, [cameraId, onDetected, onError, zoom, torch, onScannerReady, scannerRef, errorCheck, facingMode, numOfWorkers, constraints, locator, decoders, locate]);
    return null;
}

BarcodeScanner.propTypes = {
    onDetected: PropTypes.func.isRequired,
    scannerRef: PropTypes.object.isRequired,
    onScannerReady: PropTypes.func,
    cameraId: PropTypes.string,
    facingMode: PropTypes.string,
    constraints: PropTypes.object,
    locator: PropTypes.object,
    numOfWorkers: PropTypes.number,
    decoders: PropTypes.array,
    locate: PropTypes.bool,
    onError: PropTypes.func,
    zoom: PropTypes.number,
    torch: PropTypes.bool
};

const ScanResult = ({ result }) => (
    <li>
        {result.codeResult.code} [{result.codeResult.format}]
    </li>
);

ScanResult.propTypes = {
    result: PropTypes.object
};

export { ScanResult, BarcodeScanner }
