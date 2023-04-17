using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesHelpdesk.Data;
using VivesHelpdesk.Model;
using VivesHelpdesk.Services.Model.Extensions;
using VivesHelpdesk.Services.Model.Request;
using VivesHelpdesk.Services.Model.Result;

namespace VivesHelpdesk.Services
{
    public class PersonService
    {
        private readonly VivesHelpdeskDbContext _dbContext;

        public PersonService(VivesHelpdeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<PersonResult> Find()
        {
            return  _dbContext.People
                .Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    NumberOfAssignedTickets = p.AssignedTickets.Count
                })
                .ToList();
        }

        //Get
        public PersonResult? Get(int id)
        {
            return _dbContext.People
                .Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    NumberOfAssignedTickets = p.AssignedTickets.Count
                })
                .SingleOrDefault(p => p.Id == id);
        }

        //Create
        public ServiceResult<PersonResult?> Create(PersonRequest request)
        {
            var serviceResult = request.Validate();

            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }
            
            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _dbContext.Add(person);
            _dbContext.SaveChanges();

            var result = Get(person.Id);
            return new ServiceResult<PersonResult?>(result);
        }

        //Update
        public ServiceResult<PersonResult?> Update(int id, PersonRequest request)
        {
            var serviceResult = request.Validate();

            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            var person = _dbContext.People
                .SingleOrDefault(p => p.Id == id);
            if (person == null)
            {
                return new ServiceResult<PersonResult?>().NotFound(nameof(Person));
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;

            _dbContext.SaveChanges();

            var result = Get(id);
            return new ServiceResult<PersonResult?>(result);
        }

        

        //Delete
        public ServiceResult Delete(int id)
        {
            var person = _dbContext.People
                .SingleOrDefault(p => p.Id == id);
            if (person == null)
            {
                return new ServiceResult().NotFound(nameof(Person));
            }

            _dbContext.People.Remove(person);
            _dbContext.SaveChanges();

            return new ServiceResult();
        }

    }
}
