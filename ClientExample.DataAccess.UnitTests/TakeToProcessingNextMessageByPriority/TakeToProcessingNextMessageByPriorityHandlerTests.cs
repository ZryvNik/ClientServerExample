using ClientExample.Contracts.GetNextMessageByPriority;
using ClientExample.DataAccess.Entities;
using ClientExample.DataAccess.Handlers;

namespace ClientExample.DataAccess.UnitTests.TakeToProcessingNextMessageByPriority
{
    public class TakeToProcessingNextMessageByPriorityHandlerTests
    {
        [Fact(DisplayName = "Должен вернуть сообщение в статусе Added с максимальным приоритетом")]
        public async Task ReturnMessageWithMaxPriorityAndStatusAdded()
        {
            var result = default(TakeToProcessingNextMessageByPriorityResult);
            using (var context = DbHelper.GetContext())
            {
                context.Add(new InputMessage()
                {
                    Message = "test1",
                    Priority = 1,
                    Status = CrossCutting.Enums.MessageStatus.Added
                });
                context.Add(new InputMessage()
                {
                    Message = "test2",
                    Priority = 2,
                    Status = CrossCutting.Enums.MessageStatus.Added
                });
                context.Add(new InputMessage()
                {
                    Message = "test3",
                    Priority = 3,
                    Status = CrossCutting.Enums.MessageStatus.InProgress
                });
                context.SaveChanges();
                var handler = new TakeToProcessingNextMessageByPriorityHandler(context);

                result = await handler.Handle(new TakeToProcessingNextMessageByPriorityRequest(), CancellationToken.None);
            }

            Assert.Equal("test2", result.Message.Message);
            Assert.Equal(2, result.Message.Priority);
        }

        [Fact(DisplayName = "Вернуть null если нету ни одного сообщения в статусе Added")]
        public async Task ReturnNullIfNoOneMessageIsInAddedStatus()
        {
            var result = default(TakeToProcessingNextMessageByPriorityResult);
            using (var context = DbHelper.GetContext())
            {
                context.Add(new InputMessage()
                {
                    Message = "test1",
                    Priority = 1,
                    Status = CrossCutting.Enums.MessageStatus.Complete
                });
                context.Add(new InputMessage()
                {
                    Message = "test2",
                    Priority = 2,
                    Status = CrossCutting.Enums.MessageStatus.InProgress
                });
                context.Add(new InputMessage()
                {
                    Message = "test3",
                    Priority = 3,
                    Status = CrossCutting.Enums.MessageStatus.InProgress
                });
                context.SaveChanges();
                var handler = new TakeToProcessingNextMessageByPriorityHandler(context);

                result = await handler.Handle(new TakeToProcessingNextMessageByPriorityRequest(), CancellationToken.None);
            }

            Assert.Null(result.Message);
        }
    }
}
