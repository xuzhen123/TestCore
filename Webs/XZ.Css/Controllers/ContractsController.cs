using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XZ.Core.Exceptions;
using XZ.Core.Logs;
using XZ.Main.Domain;
using XZ.Main.Repository;
using XZ.Css.Commands;

namespace XZ.Css.Controllers
{
    [Authorize(Roles = "Admin,25db625b-d142-11ea-a061-de15bc72dcfe")]
    public class ContractsController : Controller
    {
        private IContractRepository _contractServices;
        private IMediator _mediator;
        private IMyLog _mylog;
        public ContractsController(IContractRepository contractServices, IMediator mediator, IMyLog mylog)
        {
            this._contractServices = contractServices;
            this._mediator = mediator;
            this._mylog = mylog;
        }

        public async Task<IActionResult> Index()//[FromServices]CreateOrder.CreateOrderClient orderClient
        {
            //throw new MyServerException("出错了", 666);

            //Merchant merchant = new Merchant(999999, 100087, "武松", 0, "CNY", 1, false, true, 7, null, 0.05M, 180, 0.01M, null, 20000, 0.03M, 0.03M, 0, 1, 2, 0, "abcdefg", 0, "新用户", 2, null, "1002", "zhangsan", DateTime.Now, DateTime.Now);

            //CreatedMerchantCommand cmc = new CreatedMerchantCommand(merchant);

            //_mediator.Send(cmc);

            //_mylog.ShowLogInfo("这是个普通的信息");
            //_mylog.ShowWarning("这是一个警告！ 请注意");
            //_mylog.ShowError("这是个错误");
            //this.ViewBag.Contracts = this._contractServices.GetContracts();//.GetContractById(327849);


            //string httpClient = await http.GetHttpResponse("https://localhost:5001/api/httptest/get");


            //var result = await orderClient.SayCreateOrderAsync(new OrderRequest { OrderId = 1001, OrderNum = "20200601094936", OrderAmount = 12.23f, DateOrderDreated = "2020" });


            return View();
        }
    }
}