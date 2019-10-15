namespace RO.Common3.Data
{
	using System;
	using System.IO;
	using System.Text;

	[SerializableAttribute]
	
	public class UsrImpr
	{
		private string pUsrs;
		private string pUsrGroups;
		private string pCultures;
		private string pRowAuthoritys;
		private string pCompanys;
		private string pProjects;
		private string pInvestors;
		private string pCustomers;
		private string pVendors;
		private string pAgents;
		private string pBrokers;
		private string pMembers;
        private string pBorrowers;
        private string pGuarantors;
        private string pLenders;

        public UsrImpr() {}

        public UsrImpr(string usrs, string usrGroups, string cultures, string rowAuthoritys, string companys, string projects, string investors, string customers, string vendors, string agents, string brokers, string members, string borrowers, string guarantors, string lenders)
		{
			pUsrs = usrs;
			pUsrGroups = usrGroups;
			pCultures = cultures;
			pRowAuthoritys = rowAuthoritys;
			pCompanys = companys;
			pProjects = projects;
			pInvestors = investors;
			pCustomers = customers;
			pVendors = vendors;
			pAgents = agents;
			pBrokers = brokers;
			pMembers = members;
            pBorrowers = borrowers;
            pGuarantors = guarantors;
            pLenders = lenders;
        }

		public string Usrs
		{
			get {return pUsrs;}
			set { pUsrs = pUsrs + (string.IsNullOrEmpty(pUsrs) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string UsrGroups
		{
			get {return pUsrGroups;}
			set { pUsrGroups = pUsrGroups + (string.IsNullOrEmpty(pUsrGroups) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string Cultures
		{
			get {return pCultures;}
			set { pCultures = pCultures + (string.IsNullOrEmpty(pCultures) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string RowAuthoritys
		{
			get {return pRowAuthoritys;}
			set { pRowAuthoritys = pRowAuthoritys + (string.IsNullOrEmpty(pRowAuthoritys) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string Companys
		{
			get {return pCompanys;}
			set { pCompanys = pCompanys + (string.IsNullOrEmpty(pCompanys) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string Projects
		{
			get { return pProjects; }
			set { pProjects = pProjects + (string.IsNullOrEmpty(pProjects) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string Investors
		{
			get {return pInvestors;}
			set { pInvestors = pInvestors + (string.IsNullOrEmpty(pInvestors) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string Customers
		{
			get {return pCustomers;}
			set { pCustomers = pCustomers + (string.IsNullOrEmpty(pCustomers) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string Vendors
		{
			get {return pVendors;}
			set { pVendors = pVendors + (string.IsNullOrEmpty(pVendors) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string Agents
		{
			get {return pAgents;}
			set { pAgents = pAgents + (string.IsNullOrEmpty(pAgents) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string Brokers
		{
			get {return pBrokers;}
			set { pBrokers = pBrokers + (string.IsNullOrEmpty(pBrokers) ? string.Empty : ((char)191).ToString()) + value; }
		}

		public string Members
		{
			get {return pMembers;}
			set { pMembers = pMembers + (string.IsNullOrEmpty(pMembers) ? string.Empty : ((char)191).ToString()) + value; }
		}

        public string Borrowers
        {
            get { return pBorrowers; }
            set { pBorrowers = pBorrowers + (string.IsNullOrEmpty(pBorrowers) ? string.Empty : ((char)191).ToString()) + value; }
        }

        public string Guarantors
        {
            get { return pGuarantors; }
            set { pGuarantors = pGuarantors + (string.IsNullOrEmpty(pGuarantors) ? string.Empty : ((char)191).ToString()) + value; }
        }

        public string Lenders
        {
            get { return pLenders; }
            set { pLenders = pLenders + (string.IsNullOrEmpty(pLenders) ? string.Empty : ((char)191).ToString()) + value; }
        }
    }
}