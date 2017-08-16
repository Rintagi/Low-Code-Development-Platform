namespace RO.Common3.Data
{
	using System;
	using System.IO;
	using System.Text;

	[SerializableAttribute]
	
	public class UsrPref
	{
		private string pSysListVisible;
		private string pPrjListVisible;
		private string pComListVisible;
		private string pMenuOption;
		private string pTopMenuCss;
		private string pTopMenuJs;
		private string pTopMenuIvk;
		private string pSidMenuCss;
		private string pSidMenuJs;
		private string pSidMenuIvk;
		private string pUsrStyleSheet;
        private string pMasterPgFile;

        public UsrPref() {}

        public UsrPref(string SysListVisible, string PrjListVisible, string ComListVisible, string MenuOption, string TopMenuCss, string TopMenuJs, string TopMenuIvk, string SidMenuCss, string SidMenuJs, string SidMenuIvk, string UsrStyleSheet, string MasterPgFile)
        {
			pSysListVisible = SysListVisible;
			pPrjListVisible = PrjListVisible;
			pComListVisible = ComListVisible;
			pMenuOption = MenuOption;
			pTopMenuCss = TopMenuCss;
			pTopMenuJs = TopMenuJs;
			pTopMenuIvk = TopMenuIvk;
			pSidMenuCss = SidMenuCss;
			pSidMenuJs = SidMenuJs;
			pSidMenuIvk = SidMenuIvk;
			pUsrStyleSheet = UsrStyleSheet;
            pMasterPgFile = MasterPgFile;
        }

		public string SysListVisible
		{
			get { return pSysListVisible; }
			set { pSysListVisible = value; }	// Need this for ObjectToXml to work.
		}

		public string PrjListVisible
		{
			get { return pPrjListVisible; }
			set { pPrjListVisible = value; }
		}

		public string ComListVisible
		{
			get {return pComListVisible;}
			set { pComListVisible = value; }
		}

		public string MenuOption
		{
			get {return pMenuOption;}
			set { pMenuOption = value; }
		}

		public string TopMenuCss
		{
			get { return pTopMenuCss; }
			set { pTopMenuCss = value; }
		}

		public string TopMenuJs
		{
			get { return pTopMenuJs; }
			set { pTopMenuJs = value; }
		}

		public string TopMenuIvk
		{
			get { return pTopMenuIvk; }
			set { pTopMenuIvk = value; }
		}

		public string SidMenuCss
		{
			get { return pSidMenuCss; }
			set { pSidMenuCss = value; }
		}

		public string SidMenuJs
		{
			get { return pSidMenuJs; }
			set { pSidMenuJs = value; }
		}

		public string SidMenuIvk
		{
			get { return pSidMenuIvk; }
			set { pSidMenuIvk = value; }
		}

		public string UsrStyleSheet
		{
			get {return pUsrStyleSheet;}
			set { pUsrStyleSheet = value; }
		}

        public string MasterPgFile
        {
            get { return pMasterPgFile; }
            set { pMasterPgFile = value; }
        }
    }
}