﻿using ApollosLibrary.Application.Common.Exceptions;
using ApollosLibrary.Application.Interfaces;
using ApollosLibrary.UnitOfWork.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApollosLibrary.Application.Series.Commands.UpdateSeriesCommand
{
    public class UpdateSeriesCommand : IRequest<UpdateSeriesCommandDto>
    {
        public int SeriesId { get; set; }
        public string Name { get; set; }
    }

    public class UpdateSeriesCommandHandler : IRequestHandler<UpdateSeriesCommand, UpdateSeriesCommandDto>
    {
        private readonly ISeriesUnitOfWork _seriesUnitOfWork;
        private readonly IUserService _userService;
        private readonly IDateTimeService _dateTimeService;

        public UpdateSeriesCommandHandler(
            ISeriesUnitOfWork seriesUnitOfWork
            , IUserService userService
            , IDateTimeService dateTimeService)
        {
            _seriesUnitOfWork = seriesUnitOfWork;
            _userService = userService;
            _dateTimeService = dateTimeService;
        }

        public async Task<UpdateSeriesCommandDto> Handle(UpdateSeriesCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateSeriesCommandDto();

            var series = await _seriesUnitOfWork.SeriesDataLayer.GetSeries(command.SeriesId);

            if (series == null)
            {
                throw new SeriesNotFoundException($"Unable to find series with id [{command.SeriesId}] for update");
            }

            series.Name = command.Name;
            series.ModifiedDate = _dateTimeService.Now;
            series.ModifiedBy = _userService.GetUserId();

            await _seriesUnitOfWork.Save();

            return response;
        }
    }
}
