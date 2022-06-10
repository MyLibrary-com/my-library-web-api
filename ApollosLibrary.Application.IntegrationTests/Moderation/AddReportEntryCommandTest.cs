﻿using ApollosLibrary.Application.Interfaces;
using ApollosLibrary.Application.Moderation.Commands.AddReportEntryCommand;
using ApollosLibrary.Domain;
using ApollosLibrary.Domain.Enums;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApollosLibrary.Application.IntegrationTests.Moderation
{
    [Collection("IntegrationTestCollection")]
    public class AddReportEntryCommandTest : TestBase
    {
        private readonly ApollosLibraryContext _context;
        private readonly IMediator _mediatr;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDateTimeService _dateTimeService;

        public AddReportEntryCommandTest(TestFixture fixture) : base(fixture)
        {
            var services = fixture.ServiceCollection;

            var mockDateTimeService = new Mock<IDateTimeService>();
            mockDateTimeService.Setup(d => d.Now).Returns(new DateTime(2021, 02, 07));
            services.AddSingleton(mockDateTimeService.Object);
            _dateTimeService = mockDateTimeService.Object;

            var provider = services.BuildServiceProvider();
            _mediatr = provider.GetRequiredService<IMediator>();
            _context = provider.GetRequiredService<ApollosLibraryContext>();
            _contextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
        }

        [Fact]
        public async Task AddReportEntryCommand()
        {
            var userID = Guid.NewGuid();

            var httpContext = new TestHttpContext
            {
                User = new TestPrincipal(new Claim[]
                {
                    new Claim("userid", userID.ToString()),
                }),
            };

            var entryCreatedUserId = Guid.NewGuid();

            _contextAccessor.HttpContext = httpContext;

            var command = new AddReportEntryCommand()
            {
                CreatedBy = entryCreatedUserId,
                EntryId = 1,
                EntryType = EntryTypeEnum.Book,
            };

            var result = await _mediatr.Send(command);

            var entry = _context.EntryReports.FirstOrDefault(e => e.EntryId == result.ReportEntryId);

            entry.Should().BeEquivalentTo(new Domain.EntryReport()
            {
                CreatedBy = entryCreatedUserId,
                EntryId = command.EntryId,
                EntryType = command.EntryType,
                ReportedBy = userID,
                ReportedDate = _dateTimeService.Now,
                EntryReportId = result.ReportEntryId,
            });
        }
    }
}
