﻿namespace RocketNotify.TelegramBot.Tests.MessageProcessing.Start
{
    using System.Threading.Tasks;

    using Moq;

    using NUnit.Framework;

    using RocketNotify.TelegramBot.Client;
    using RocketNotify.TelegramBot.MessageProcessing.Model;
    using RocketNotify.TelegramBot.MessageProcessing.Start;

    [TestFixture]
    public class StartCommandProcessorTests
    {
        private Mock<ITelegramMessageSender> _responder;

        private StartCommandProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _responder = new Mock<ITelegramMessageSender>();
            _processor = new StartCommandProcessor(_responder.Object);
        }

        [TestCase("")]
        [TestCase("Test text")]
        [TestCase("Test text with a 'Start' word in it")]
        public void IsRelevant_DoesNotContainCommand_ShouldReturnFalse(string text)
        {
            var message = new BotMessage { Text = text };
            var actual = _processor.IsRelevant(message);

            Assert.False(actual);
        }

        [TestCase("/start")]
        [TestCase("Test text with a /Start command in it")]
        [TestCase("/start@bot_name")]
        [TestCase("/start @bot_name")]
        [TestCase("@bot_name /start")]
        public void IsRelevant_ContainsCommand_ShouldReturnFalse(string text)
        {
            var message = new BotMessage { Text = text };
            var actual = _processor.IsRelevant(message);

            Assert.True(actual);
        }

        [Test]
        public async Task ProcessAsync_ShouldSendMessage()
        {
            var message = new BotMessage { Sender = new MessageSender { Id = 1, Name = "User" }, Text = "/start" };
            var result = await _processor.ProcessAsync(message).ConfigureAwait(false);

            Assert.True(result.IsFinal);
            _responder.Verify(x => x.SendMessageAsync(message.Sender.Id, It.IsAny<string>()), Times.Once);
        }
    }
}
