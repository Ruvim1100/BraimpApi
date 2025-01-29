using AutoMapper;
using Braimp.Application.Courses.Commands.CreateCourse;
using Braimp.Application.Courses.Commands.DeleteCourse;
using Braimp.Application.Courses.Commands.UpdateCourse;
using Braimp.Application.Courses.Queries.GetCourseDetails;
using Braimp.Application.Courses.Queries.GetCourseList;
using Braimp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : BaseController
    {
        private readonly IMapper _mapper;
        public CourseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CourseListVm>> GetAll()
        {
            var vm = await Mediator.Send(new GetCourseListQuery());
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDetailsVm>> Get(Guid id)
        {
            var query = new GetCourseDetailQuery()
            {
                Id = id,
                OwnerId = UserId
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCourseDto createCourseDto)
        {
            var command = _mapper.Map<CreateCourseCommand>(createCourseDto);
            command.OwnerId = UserId;
            var courseId = await Mediator.Send(command);
            return Ok(courseId);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateCourseDto updateCourseDto)
        {
            var command = _mapper.Map<UpdateCourseCommand>(updateCourseDto);
            command.OwnerId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteCourseCommand
            {
                Id = id,
                OwnerId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
