using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CfsInvestmentCalculator.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using static CfsInvestmentCalculator.Controllers.InvestmentCalculatorController;


namespace CfsInvestmentCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorControllers : ControllerBase
    {

        [HttpPost(Name = "GetInvestmentReturns")]
        public InvestmentReturn CalculateInvestmentReturn(List<Calculator> ListInvestmentOpt, int TotalAmt)
        {
            try
            {
                Exception ex = null;
                if (TotalAmt > 0)
                {
                    var cn = new InvestmentCalculatorController();
                    double ROIAmt = 0;
                    double Fees = 0;
                    double InvAmt = 0;

                    foreach (var inv in ListInvestmentOpt)
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
                        inv.RIOAmt = (decimal)ROIAmt;
                        inv.Fees = (decimal)Fees;
                    }

                    InvestmentReturn objInvOnReturn;

                    if (objInvOnReturn.InvstFees > 0)
                    {

                        var result = cn.convertRate(objInvOnReturn.InvstFees);
                        objInvOnReturn.InvstFees = result;
                    }
                    else
                        objInvOnReturn.InvstFees = 0;

                    return objInvOnReturn;
                }
                else
                    return new InvestmentReturn(0,0,ex);
            }
            catch (Exception ex)
            {

                return new InvestmentReturn(0,0,ex);

            }
        }
    }
}
