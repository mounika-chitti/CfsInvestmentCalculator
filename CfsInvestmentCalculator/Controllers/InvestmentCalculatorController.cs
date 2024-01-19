using CfsInvestmentCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CfsInvestmentCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestmentCalculatorController : Controller
    {
        public enum InvOpt
        {
            Cash,
            Fixed,
            Shares,
            Managed,
            Exchange,
            Bonds,
            Annuities,
            LIC,
            REIT
        }
        public int convertRate(double intAmt)
        {
            try
            {
                var client = new RestClient("https://api.apilayer.com/");
                var request = new RestRequest("exchangerates_data/convert?to=USD&from=AUD&apikey=4rn8hqxWUNI4t1r2AzaF3x4J9IThrvfs", Method.Get);
                request.AddQueryParameter("amount", intAmt);


                RestResponse response = client.Execute(request);
                var result = 0;
                if (response.Content != null)
                {
                    var obj = JObject.Parse(response.Content);
                    if (obj != null)
                    {
                        result = (int)obj.SelectToken("result");
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                return -20;
            }
        }

        public Investment CalculateInvestment(List<Calculator> Investment, int TotalAmt, Exception? ex)
        {
            double ROIAmt = 0;
            double Fees = 0;
            double InvAmt = 0;
            try
            {
                if (TotalAmt > 0)
                {
                    foreach (var inv in Investment)
                    {
                        InvAmt = 0;
                        InvAmt = (double)(TotalAmt * inv.InvestmentPercentage / 100);
                        if (inv.InvestmentOption.ToString() == "Cash")
                        {
                            ROIAmt += ((inv.InvestmentPercentage <= 50) ? (TotalAmt * 8.5 / 100) : (TotalAmt * 10 / 100));
                            Fees += ((inv.InvestmentPercentage <= 50) ? (InvAmt * 0.5 / 100) : 0);
                        }
                        else if (inv.InvestmentOption.ToString() == "Fixed")
                        {
                            ROIAmt += (TotalAmt * 10 / 100);
                            Fees += (InvAmt * 1 / 100);
                        }
                        else if (inv.InvestmentOption.ToString() == "Shares")
                        {
                            Fees += InvAmt * 2.5 / 100;
                            ROIAmt += ((inv.InvestmentPercentage <= 70) ? (TotalAmt * 4.3 / 100) : (TotalAmt * 6 / 100));
                        }
                        else if (inv.InvestmentOption.ToString() == "Managed")
                        {
                            ROIAmt += (TotalAmt * 12 / 100);
                            Fees += (InvAmt * 0.3 / 100);
                        }
                        else if (inv.InvestmentOption.ToString() == "Exchange")
                        {
                            Fees += InvAmt * 2 / 100;
                            ROIAmt += ((inv.InvestmentPercentage <= 40) ? (TotalAmt * 12.8 / 100) : (TotalAmt * 40 / 100));
                        }
                        else if (inv.InvestmentOption.ToString() == "bonds")
                        {
                            ROIAmt += (TotalAmt * 8 / 100);
                            Fees += (InvAmt * 0.9 / 100);
                        }
                        else if (inv.InvestmentOption.ToString() == "Annuities")
                        {
                            ROIAmt += (TotalAmt * 4 / 100);
                            Fees += (InvAmt * 1.4 / 100);
                        }
                        else if (inv.InvestmentOption.ToString() == "LIC")
                        {
                            ROIAmt += (TotalAmt * 6 / 100);
                            Fees += (InvAmt * 1.3 / 100);
                        }
                        else if (inv.InvestmentOption.ToString() == "REIT")
                        {
                            ROIAmt += (TotalAmt * 4 / 100);
                            Fees += (InvAmt * 2 / 100);
                        }
                        //inv.RIOAmt = (decimal)ROIAmt;
                        //inv.Fees = (decimal)Fees;
                    }
                }
                InvestmentReturn objInvOnReturn = new InvestmentReturn(TotalAmt + ROIAmt, Fees,ex);

                return objInvOnReturn;
            }
            catch (Exception ex)
            {
                return ex;
            }
            }

    }
}
