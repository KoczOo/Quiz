using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WebApplication1.Controllers
{

    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuizContext _quizContext;

        public QuestionController(QuizContext context)
        {
            _quizContext = context;
        }
                    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> Get()
        {
            return await _quizContext.Questions.ToListAsync();
        }

        [HttpGet("{quizId}")]
        public async Task<ActionResult<IEnumerable<Question>>> Get([FromRoute] int quizId)
        {
            return await _quizContext.Questions.Where(q => q.QuizID == quizId).ToListAsync();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Question question)
        {
            _quizContext.Questions.Add(question);
            await _quizContext.SaveChangesAsync();
            return Ok(question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put (int id, [FromBody] Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            _quizContext.Entry(question).State = EntityState.Modified;

            await _quizContext.SaveChangesAsync();

            return Ok(question);
        }
    }
}   