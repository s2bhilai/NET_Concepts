﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawCoding_IntegrationTesting.Introduction
{
    public class AnimalRegistration
    {
        private readonly AppDbContext _ctx;
        private readonly IEventSink _eventSink;
        private readonly ISystemClock _clock;

        public AnimalRegistration(
            AppDbContext ctx,
            IEventSink eventSink,
            ISystemClock clock
        )
        {
            _ctx = ctx;
            _eventSink = eventSink;
            _clock = clock;
        }

        public async Task Register(Animal animal)
        {
            animal.Created = _clock.Now();
            _ctx.Add(animal);
            await _ctx.SaveChangesAsync();

            _eventSink.SendEmailsToCustomerThatWantThisTypeOfAnimalsAndAttachPicturesAndAllThatJazz();
        }
    }
}
