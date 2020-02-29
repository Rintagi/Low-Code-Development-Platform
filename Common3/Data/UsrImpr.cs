namespace RO.Common3.Data
{
	using System;
	using System.IO;
	using System.Text;
    using System.Collections.Generic;
    using System.Linq;

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
        private string Merge(string ls1, string ls2)
        {
            List<string> lo = new List<string>();
            HashSet<string> v = new HashSet<string>();
            string[] l1 = (ls1 ?? "").Split(new char[] { (char)191 }, StringSplitOptions.RemoveEmptyEntries);
            string[] l2 = (ls2 ?? "").Split(new char[] { (char)191 }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in l1)
            {
                if (!v.Contains(s)) {
                    lo.Add(s);
                    v.Add(s);
                }
            }
            foreach (string s in l2)
            {
                if (!v.Contains(s))
                {
                    lo.Add(s);
                    v.Add(s);
                }
            }

            return string.Join(((char)191).ToString(), lo.ToArray());

        }
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
            set { pUsrs = Merge(pUsrs, value);}
		}

		public string UsrGroups
		{
			get {return pUsrGroups;}
			set { pUsrGroups = Merge(pUsrGroups,value); }
		}

		public string Cultures
		{
			get {return pCultures;}
			set { pCultures = Merge(pCultures,value); }
		}

		public string RowAuthoritys
		{
			get {return pRowAuthoritys;}
			set { pRowAuthoritys = Merge(pRowAuthoritys,value); }
		}

		public string Companys
		{
			get {return pCompanys;}
			set { pCompanys = Merge(pCompanys,value); }
		}

		public string Projects
		{
			get { return pProjects; }
			set { pProjects = Merge(pProjects,value); }
		}

		public string Investors
		{
			get {return pInvestors;}
			set { pInvestors = Merge(pInvestors,value); }
		}

		public string Customers
		{
			get {return pCustomers;}
			set { pCustomers = Merge(pCustomers,value); }
		}

		public string Vendors
		{
			get {return pVendors;}
			set { pVendors = Merge(pVendors,value); }
		}

		public string Agents
		{
			get {return pAgents;}
			set { pAgents = Merge(pAgents,value); }
		}

		public string Brokers
		{
			get {return pBrokers;}
			set { pBrokers = Merge(pBrokers,value); }
		}

		public string Members
		{
			get {return pMembers;}
			set { pMembers = Merge(pMembers,value); }
		}

        public string Borrowers
        {
            get { return pBorrowers; }
            set { pBorrowers = Merge(pBorrowers,value); }
        }

        public string Guarantors
        {
            get { return pGuarantors; }
            set { pGuarantors = Merge(pGuarantors,value); }
        }

        public string Lenders
        {
            get { return pLenders; }
            set { pLenders = Merge(pLenders,value); }
        }
    }
}