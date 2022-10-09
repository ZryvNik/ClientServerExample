using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample.Infrustructure.UnitTests.InputMessageProcessing
{
    public class InputMessageProcessingServiceTests
    {
        [Fact(DisplayName = "Нет вызовов методов, если нет добавленных сообщений")]
        public async Task NoCallsexecutableMethodsIfNoMessages()
        {
            var mediatr = new Mock<ISender>();

            var handler = new InputMessageProcessingService(mediatr.Object);

            var result = await handler.Handle(new Contracts.AddMessages.GenerateOutputMessagesRequest()
            {
                Count = count
            }, CancellationToken.None);

            Assert.Equal(count, result.OutputMessages.Count());
        }

        [Fact(DisplayName = "Возвращает коллекцию из задданного количества элементов")]
        public async Task ReturnCollectionWithGivenNumberOfElements(int count)
        {
            var bus = new Mock<IBusPublisher>().Object;

            var handler = new GenerateOutputMessagesHandler(bus);

            var result = await handler.Handle(new Contracts.AddMessages.GenerateOutputMessagesRequest()
            {
                Count = count
            }, CancellationToken.None);

            Assert.Equal(count, result.OutputMessages.Count());
        }

        [Fact(DisplayName = "Возвращает коллекцию из задданного количества элементов")]
        public async Task ReturnCollectionWithGivenNumberOfElements(int count)
        {
            var bus = new Mock<IBusPublisher>().Object;

            var handler = new GenerateOutputMessagesHandler(bus);

            var result = await handler.Handle(new Contracts.AddMessages.GenerateOutputMessagesRequest()
            {
                Count = count
            }, CancellationToken.None);

            Assert.Equal(count, result.OutputMessages.Count());
        }
    }
}
