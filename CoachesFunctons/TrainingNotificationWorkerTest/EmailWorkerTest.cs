using System;
using Castle.Components.DictionaryAdapter;
using InformationService.DataModels;
using InformationService.Interfaces;
using Moq;
using TrainingNotificationWorker;
using Xunit;

namespace TrainingNotificationWorkerTest
{
    public class EmailWorkerTest
    {
        [Theory]
        [InlineData(1, 7)]
        public void GetEmailsForSport_When_executed_create_list_of_SportPhones(int sportId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            //InitializeRegistrants();
            //LoadRegistrants();
            var mock = new Mock<ITrainingRepository>();
            mock.Setup(repository => repository.GetEmailsBySport(sportId)).ReturnsAsync(new EditableList<SportEmails>());
            var worker = new EmailWorker("connection string");
            var actual = worker.GetEmailsForSport(sportId);

            Assert.Equal(expected, actual.Result.Count);

        }
    }
}
