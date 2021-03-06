﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RSG.Tests
{
    public class PromiseTimerTests
    {
        [Fact]
        public void wait_for_doesnt_resolve_before_specified_time()
        {
            var testObject = new PromiseTimer();

            var testTime = 2f;
            var hasResolved = false;

            testObject.WaitFor(testTime)
                .Then(() => hasResolved = true)
                .Done();

            testObject.Update(1f);

            Assert.Equal(false, hasResolved);
        }

        [Fact]
        public void wait_for_resolves_after_specified_time()
        {
            var testObject = new PromiseTimer();

            var testTime = 1f;
            var hasResolved = false;

            testObject.WaitFor(testTime)
                .Then(() => hasResolved = true)
                .Done();

            testObject.Update(2f);

            Assert.Equal(true, hasResolved);
        }

        [Fact]
        public void wait_until_resolves_when_predicate_is_true()
        {
            var testObject = new PromiseTimer();

            var hasResolved = false;

            var doResolve = false;

            testObject.WaitUntil(timeData => doResolve)
                .Then(() => hasResolved = true)
                .Done();

            Assert.Equal(false, hasResolved);

            doResolve = true;
            testObject.Update(1f);

            Assert.Equal(true, hasResolved);
        }

        [Fact]
        public void wait_while_resolves_when_predicate_is_false()
        {
            var testObject = new PromiseTimer();

            var hasResovled = false;

            var doWait = true;

            testObject.WaitWhile(timeData => doWait)
                .Then(() => hasResovled = true)
                .Done();

            Assert.Equal(false, hasResovled);

            doWait = false;
            testObject.Update(1f);

            Assert.Equal(true, hasResovled);
        }
    }

}
