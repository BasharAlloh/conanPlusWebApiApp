using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using conanPlusWebApiApp.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly ICommonRepository<Goal> _goalRepository;
        private readonly IMapper _mapper;

        public GoalController(ICommonRepository<Goal> goalRepository, IMapper mapper)
        {
            _goalRepository = goalRepository;
            _mapper = mapper;
        }

        // Get all Goals
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGoals()
        {
            try
            {
                var goals = await _goalRepository.GetAll();
                if (goals == null || !goals.Any())
                {
                    return NotFound("No goals found.");
                }

                var goalDtos = _mapper.Map<IEnumerable<GoalDTO>>(goals);
                return Ok(goalDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get a Goal by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGoalById(int id)
        {
            try
            {
                var goal = await _goalRepository.GetDetails(id);
                if (goal == null)
                {
                    return NotFound("Goal not found.");
                }

                var goalDto = _mapper.Map<GoalDTO>(goal);
                return Ok(goalDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Create a new Goal
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGoal(GoalCreateDTO goalCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newGoal = _mapper.Map<Goal>(goalCreateDto);
                var createdGoal = await _goalRepository.Insert(newGoal);

                var goalDto = _mapper.Map<GoalDTO>(createdGoal);
                return CreatedAtAction(nameof(GetGoalById), new { id = createdGoal.GoalId }, goalDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update an existing Goal
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGoal(int id, GoalUpdateDTO goalUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goalUpdateDto.GoalId)
            {
                return BadRequest("Goal ID mismatch.");
            }

            try
            {
                var existingGoal = await _goalRepository.GetDetails(id);
                if (existingGoal == null)
                {
                    return NotFound("Goal not found.");
                }

                _mapper.Map(goalUpdateDto, existingGoal);
                var updatedGoal = await _goalRepository.Update(existingGoal);

                var goalDto = _mapper.Map<GoalDTO>(updatedGoal);
                return Ok(goalDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete a Goal by ID
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGoal(int id)
        {
            try
            {
                var goal = await _goalRepository.GetDetails(id);
                if (goal == null)
                {
                    return NotFound("Goal not found.");
                }

                await _goalRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
