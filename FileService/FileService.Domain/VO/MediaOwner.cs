using CSharpFunctionalExtensions;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Domain.VO
{
    public record MediaOwner
    {
        private MediaOwner(Guid entiteId, string context)
        {
            EntiteId = entiteId;
            Context = context;
        }

        public Guid EntiteId { get; }

        public string Context { get; }

        public static Result<MediaOwner, Error> Create(Guid entiteId, string context)
        {
            if (string.IsNullOrWhiteSpace(context) || context.Length <= 50)
                return GeneralErrors.ValueNotValid("context");
            context = context.Trim().ToLower();
            if (entiteId == Guid.Empty)
                return GeneralErrors.ValueNotValid("ownerId");
            return new MediaOwner(entiteId, context);
        }

        public static Result<MediaOwner, Error> ToLesson(Guid LessonId) => new MediaOwner(LessonId, "Lesson");

        public static Result<MediaOwner, Error> ToDepartament(Guid DepartamentId) => new MediaOwner(DepartamentId, "Departament");

        public static Result<MediaOwner, Error> ToCourse(Guid CourseId) => new MediaOwner(CourseId, "Course");

    }
}
