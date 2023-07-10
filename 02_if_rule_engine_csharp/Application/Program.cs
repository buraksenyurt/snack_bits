using Domain;
using RulesEngine.Models;

public class Program
{
    static void Main()
    {
        var monitor = new Product
        {
            Id = 1,
            Title = "Widescreen 42 inch Full HD Curve monitor",
            ListPrice = 1000M,
            StockLevel = 12
        };

        var orders = new List<Order>
            {
                new Order
                {
                    Id = 1001,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.AuthorizedDealer,
                    ProductId = monitor.Id,
                    Quantity = 2
                },
                new Order
                {
                    Id = 1002,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.Individual,
                    ProductId = monitor.Id,
                    Quantity = 2
                },
                new Order
                {
                    Id = 1003,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.GroupFirm,
                    ProductId = monitor.Id,
                    Quantity = 3
                },
                new Order
                {
                    Id = 1004,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.Distributor,
                    ProductId = monitor.Id,
                    Quantity = 1
                },
                new Order
                {
                    Id = 1005,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.Seller,
                    ProductId = monitor.Id,
                    Quantity = 1
                }
            };

        #region Initial State 

        // foreach (var order in orders)
        // {
        //     if (order.Quantity < monitor.StockLevel)
        //     {
        //         int discountRate;
        //         switch (order.CustomerType)
        //         {
        //             case CustomerType.AuthorizedDealer:
        //                 discountRate = 2;
        //                 break;
        //             case CustomerType.Distributor:
        //                 discountRate = 5;
        //                 break;
        //             case CustomerType.Individual:
        //                 discountRate = 3;
        //                 break;
        //             case CustomerType.GroupFirm:
        //                 discountRate = 18;
        //                 break;
        //             default:
        //                 discountRate = 0;
        //                 break;
        //         }
        //         Console.WriteLine($"İndirim tutarı % {discountRate}");
        //     }
        // }

        #endregion

        #region Rule Engine State

        var workflows = new List<Workflow>() {
                new Workflow
                    {
                        WorkflowName = "Discounting Rules",
                        Rules = new List<Rule>
                        {
                            new Rule
                            {
                                RuleName="Individual Order",
                                Expression = "input2.Quantity < input1.StockLevel && input2.CustomerType == CustomerType.Individual",
                                RuleExpressionType = RuleExpressionType.LambdaExpression,
                                Actions = new RuleActions
                                {
                                    OnSuccess= new ActionInfo
                                    {
                                        Name = "OutputExpression",
                                        Context = new Dictionary<string, object>()
                                        {
                                            { "Expression",3 }
                                        }
                                    }
                                }
                            },
                            new Rule
                            {
                                RuleName="Distributor Order",
                                Expression = "input2.Quantity < input1.StockLevel && input2.CustomerType == CustomerType.Distributor",
                                RuleExpressionType = RuleExpressionType.LambdaExpression,
                                Actions = new RuleActions
                                {
                                    OnSuccess= new ActionInfo
                                    {
                                        Name = "OutputExpression",
                                        Context = new Dictionary<string, object>()
                                        {
                                            { "Expression",5 }
                                        }
                                    }
                                }
                            },
                            new Rule
                            {
                                RuleName="Authorized Dealer",
                                Expression = "input2.Quantity < input1.StockLevel && input2.CustomerType == CustomerType.AuthorizedDealer",
                                RuleExpressionType = RuleExpressionType.LambdaExpression,
                                Actions = new RuleActions
                                {
                                    OnSuccess= new ActionInfo
                                    {
                                        Name = "OutputExpression",
                                        Context = new Dictionary<string, object>()
                                        {
                                            { "Expression",2 }
                                        }
                                    }
                                }
                            },
                            new Rule
                            {
                                RuleName="Group Firm",
                                Expression = "input2.Quantity < input1.StockLevel && input2.CustomerType == CustomerType.GroupFirm",
                                RuleExpressionType = RuleExpressionType.LambdaExpression,
                                Actions = new RuleActions
                                {
                                    OnSuccess= new ActionInfo
                                    {
                                        Name = "OutputExpression",
                                        Context = new Dictionary<string, object>()
                                        {
                                            { "Expression",18 }
                                        }
                                    }
                                }
                            }
                        }
                }
            };

        var ruleEngine = new RulesEngine.RulesEngine(workflows.ToArray());
        foreach (var order in orders)
        {
            var inputs = new dynamic[]
                        {
                                monitor,
                                order,
                                order.CustomerType
                        };
            var results = ruleEngine.ExecuteAllRulesAsync("Discounting Rules", inputs).Result;
            foreach (var ruleResult in results)
            {
                if (ruleResult.ActionResult != null && ruleResult.IsSuccess)
                {
                    Console.WriteLine($"'{ruleResult.Rule.RuleName}' için beklenen indirim oranı '% {ruleResult.ActionResult.Output}'");
                }
            }

            //results.OnSuccess((eventName) =>
            //{
            //    Console.WriteLine($"'{eventName}' isimli olay tetiklendi. Sipariş durumu");
            //});

            //results.OnFail(() =>
            //{
            //    Console.WriteLine("Sipariş verilemez");
            //});
        }

        #endregion

    }
}

namespace Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public decimal ListPrice { get; set; }
        public int StockLevel { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CustomerType CustomerType { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public enum CustomerType
    {
        AuthorizedDealer,
        Distributor,
        Individual,
        GroupFirm,
        Seller
    }
}

