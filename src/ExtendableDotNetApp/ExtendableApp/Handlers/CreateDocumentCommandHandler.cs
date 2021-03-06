using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharedEventsAndCommands;

namespace ExtendableApp.Handlers
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand>
    {
        private readonly IMediator _mediator;

        public CreateDocumentCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Unit> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Document creation ---------------------->");
            // do some work
            Console.WriteLine("Document created!!!");
            
            //publishes notification that document was created
            //Note: naive background call
            Task.Factory.StartNew(() => 
                _mediator.Publish(new DocumentCreated
                {
                    Name = request.Name,
                    Author = request.Author,
                    CreatedAt = DateTime.Now
                }, cancellationToken), cancellationToken);
            Console.WriteLine("Document creation <----------------------");
            return Unit.Task;
        }
    }
}