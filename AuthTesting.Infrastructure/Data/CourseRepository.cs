using AuthTesting.Application.Abstractions.Repositories;
using AuthTesting.Domain.Entities;

namespace AuthTesting.Infrastructure.Data;

public class CourseRepository : ICourseRepository
{
	private static readonly Course _course1 = new()
	{
		Id = 1,
		Name = "Course 1"
	};

	private static readonly Course _course2 = new()
	{
		Id = 2,
		Name = "Course 2"
	};

	private static readonly Course _course3 = new()
	{
		Id = 3,
		Name = "Course 3"
	};

	private readonly IEnumerable<Course> _courses = new List<Course>
	{
		_course1,
		_course2,
		_course3
	};
	
	public async Task<Course?> GetByIdAsync(int id)
	{
		await Task.Delay(100); //long work

		foreach (Course course in _courses)
		{
			if (course.Id == id)
			{
				return course;
			}
		}

		return null;
	}
}