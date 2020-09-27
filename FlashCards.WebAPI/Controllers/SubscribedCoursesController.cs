using AutoMapper;
using FlashCards.Services.Abstracts;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.UnitOfWork.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribedCoursesController : ControllerBase
    {
        private readonly ISubscribedCourseRepository _subscribedCourseRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public SubscribedCoursesController(ISubscribedCourseRepository subscribedCourseRepository, 
            IMapper mapper, IAuthService authService)
        {
            _subscribedCourseRepository = subscribedCourseRepository;
            _mapper = mapper;
            _authService = authService;
        }
    }
}
