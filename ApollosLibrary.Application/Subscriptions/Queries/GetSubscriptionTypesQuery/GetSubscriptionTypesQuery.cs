﻿using ApollosLibrary.Application.Common.Enums;
using ApollosLibrary.UnitOfWork.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApollosLibrary.Application.Subscriptions.Queries.GetSubscriptionTypesQuery
{
    public class GetSubscriptionTypesQuery : IRequest<GetSubsriptionTypesQueryDto>
    {
    }

    public class GetSubscriptionTypesQueryHandler : IRequestHandler<GetSubscriptionTypesQuery, GetSubsriptionTypesQueryDto>
    {
        private readonly ISubscriptionUnitOfWork _subscriptionUnitOfWork;

        public GetSubscriptionTypesQueryHandler(ISubscriptionUnitOfWork subscriptionUnitOfWork)
        {
            _subscriptionUnitOfWork = subscriptionUnitOfWork;
        }

        public async Task<GetSubsriptionTypesQueryDto> Handle(GetSubscriptionTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _subscriptionUnitOfWork.SubscriptionDataLayer.GetSubscriptionTypes();

            return new GetSubsriptionTypesQueryDto()
            {
                SubscriptionTypes = types.Select(t => new SubscriptionTypeDTO()
                {
                    Cost = t.MonthlyRate,
                    MaxUsers = t.MaxUsers,
                    SubscriptionName = t.SubscriptionName,
                    SubscriptionType = (SubscriptionTypeEnum)t.SubscriptionTypeId,
                })
                .ToList(),
            };
        }
    }
}