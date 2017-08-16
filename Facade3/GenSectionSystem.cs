namespace RO.Facade3
{
	using System;
	using System.Data;
	using RO.Common3;
	using RO.Common3.Data;
	using RO.Rule3;

	public class GenSectionSystem : MarshalByRefObject
	{
        public bool CreateProgram(string SectionCd, CurrPrj CPrj, CurrSrc CSrc)
		{
            return (new GenSectionRules()).CreateProgram(SectionCd, CPrj, CSrc);
		}
	}
}