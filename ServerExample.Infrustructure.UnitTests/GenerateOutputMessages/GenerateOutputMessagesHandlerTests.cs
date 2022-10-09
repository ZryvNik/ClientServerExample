using Moq;
using ServerExample.Infrustructure.Handlers;
using ServerExample.Infrustructure.MassTransit;
using ServerExample.Infrustructure.MassTransit.Events;

namespace ServerExample.Infrustructure.UnitTests.GenerateOutputMessages
{
    public class GenerateOutputMessagesHandlerTests
    {
        [Theory(DisplayName = "Возвращает коллекцию из задданного количества элементов")]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(1000)]
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

        [Theory(DisplayName = "Метод publish вызывается заданное количество раз")]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(1000)]
        public async Task CallPublishGivenTimes(int count)
        {
            var bus = new Mock<IBusPublisher>();

            var handler = new GenerateOutputMessagesHandler(bus.Object);

            var result = await handler.Handle(new Contracts.AddMessages.GenerateOutputMessagesRequest()
            {
                Count = count
            }, CancellationToken.None);

            bus.Verify(x => x.Pubish(It.IsAny<OutputMessageEvent>(), It.IsAny<CancellationToken>()), Times.Exactly(count));
        }
    }
}
