using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace Install
{
	public class ItemNDEV
	{
        public void InstCln(string Svr, string TarPath, string WsPath, string XlsPath, string WsUrl, string newNS, Action<int, string> progress)
        {
        }
        public void InstRul(string TarPath, string newNS, Action<int, string> progress)
        {
        }
        public void InstSysM(string Svr, string Usr, string Pwd, string newNS, Action<int, string> progress, string serverVer, string dbPath, string encKey, string AppUsr, string AppPwd, bool bIntegratedSecurity)
        {
        }
        public void InstSysS(string Svr, string Usr, string Pwd, string newNS, Action<int, string> progress, string serverVer, string dbPath, string encKey, string AppUsr, string AppPwd, bool bIntegratedSecurity)
        {
        }
        public void InstAppM(string Svr, string Usr, string Pwd, string newNS, Action<int, string> progress, string serverVer, string dbPath, string encKey, string AppUsr, string AppPwd, bool bIntegratedSecurity)
        {
        }
        public void InstAppS(string Svr, string Usr, string Pwd, string newNS, Action<int, string> progress, string serverVer, string dbPath, string encKey, string AppUsr, string AppPwd, bool bIntegratedSecurity)
        {
        }
        public void InstDesM(string Svr, string Usr, string Pwd, string newNS, Action<int, string> progress, string serverVer, string dbPath, string encKey, string AppUsr, string AppPwd, bool bIntegratedSecurity)
        {
        }
        public void InstDesS(string Svr, string Usr, string Pwd, string newNS, Action<int, string> progress, string serverVer, string dbPath, string encKey, string AppUsr, string AppPwd, bool bIntegratedSecurity)
        {
        }
        public void ApplyDesChg(string DbProvider, string Svr, string Usr, string Pwd, string newNS, string clientTierPath, string ruleTierPath, string WsTierPath, Action<int, string> progress, bool bIntegratedSecurity)
        {
        }
        public void ApplyAppChg(string DbProvider, string Svr, string Usr, string Pwd, string newNS, string clientTierPath, string ruleTierPath, string WsTierPath, Action<int, string> progress, bool bIntegratedSecurity)
        {
        }
    }
}
