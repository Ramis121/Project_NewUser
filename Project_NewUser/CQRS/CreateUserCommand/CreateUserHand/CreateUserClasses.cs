using MediatR;
using Project_NewUser.Interface;
using Project_NewUser.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Project_NewUser.CQRS.CreateUserCommand.CreateUserHand
{
    public class CreateUserClasses : IRequest<User>
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUserClasses, User>
    {
        private readonly IRepositoryUser _repositoryUser;
        public CreateUserHandler(IRepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public async Task<User> Handle(CreateUserClasses request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.name,
                age = request.age,
                Id = request.id,
            };
            return await _repositoryUser.AddUser(user);
        }
    }
}
