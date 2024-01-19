
namespace CfsInvestmentCalculator.Models
{
	public class InvestmentReturn
	{

        public InvestmentReturn(double invReturn, double invFees, Exception ex)
		{
			InvstReturn = invReturn;
			InvstFees = invFees;
			//errmsg = errmsg;
		}

        public double InvstReturn { get; set; }

		public double InvstFees { get; set; }
		//public string errmsg { get; set; }

	}
}
