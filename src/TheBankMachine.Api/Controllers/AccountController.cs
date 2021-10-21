using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheBankMachine.Api.Application;
using TheBankMachine.Api.Models;
using TheBankMachine.Domain.Exceptions;

namespace TheBankMachine.Api.Controllers.Account
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        //TODO: Authentication with JWTs

        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("api/account/create")]
        [HttpPost]
        public async Task<IActionResult> AccountCreate(AccountViewModel accountViewModel)
        {
            //TODO: Add account duplicate check
            var accountCreateCommand = new AccountCreateCommand();
            accountCreateCommand.Balance = accountViewModel.Balance;
            accountCreateCommand.PinCode = accountViewModel.PinCode;
            accountCreateCommand.AccountNumber = accountViewModel.AccountNumber;

            try
            {
                var result = await _mediator.Send(accountCreateCommand);
                return Ok("Account Created Successfully");
            }
            catch(AccountException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/account/deposit")]
        [HttpPost]
        public async Task<IActionResult> AccountDeposit(AccountDepositViewModel accountDepositViewModel)
        {
            var accountDepositCommand = new AccountDepositCommand();
            accountDepositCommand.Amount = accountDepositViewModel.Amount;
            accountDepositCommand.PinCode = accountDepositViewModel.PinCode;
            accountDepositCommand.AccountNumber = accountDepositViewModel.AccountNumber;

            try
            {
                var result = await _mediator.Send(accountDepositCommand);
                return Ok(result);
            }
            catch (AccountException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/account/withdraw")]
        [HttpPost]
        public async Task<IActionResult> AccountWithdraw(AccountWithDrawViewModel accountWithDrawViewModel)
        {
            var accountWithDrawCommand = new AccountWithdrawCommand();
            accountWithDrawCommand.Amount = accountWithDrawViewModel.Amount;
            accountWithDrawCommand.PinCode = accountWithDrawViewModel.PinCode;
            accountWithDrawCommand.AccountNumber = accountWithDrawViewModel.AccountNumber;

            try
            {
                var result = await _mediator.Send(accountWithDrawCommand);
                return Ok(result);
            }
            catch (AccountException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
