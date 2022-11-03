using Microsoft.AspNetCore.Mvc;
using movie_app_graduation_api.Models.DTOs;
using MoviesApi.Helper;

namespace movie_app_graduation_api.Controllers.CalculatorController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly MoviesDbContext _dbContext;
        private readonly ICalculatorServiceHelper _helper;

        public CalculatorController(MoviesDbContext dbContext, ICalculatorServiceHelper calculatorServiceHelper)
        {
            this._helper = new CalculatorServiceHelper();
            _dbContext = dbContext;
        }

        [HttpPost("Add")]
        public async Task<int> AddTwoNumbers([FromBody] CalculatorDto dTO)
        {
            int res = 0;
            try
            {
                res = await _helper.AddAsync(dTO.number1, dTO.number2);
                _dbContext.Add(new Calculator
                {
                    Number1 = dTO.number1,
                    Number2 = dTO.number2,
                    OperationName = "Add"
                });
                _dbContext.SaveChanges();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return res;

        }
        [HttpPost("GetSub")]
        public async Task<int> SubOfTwoNumbers([FromBody] CalculatorDto dTO)
        {
            var res = await _helper.Subtract(dTO.number1, dTO.number2);
            _dbContext.Add(new Calculator
            {
                Number1 = dTO.number1,
                Number2 = dTO.number2,
                OperationName = "Subtract"
            });
            _dbContext.SaveChanges();
            return res;

        }


        [HttpGet("GetCalcs")]
        public async Task<List<Calculator>> GetCalculators()
        {
            return await _dbContext.Calculators.ToListAsync();
        }

    }
}
