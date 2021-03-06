﻿using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
using MoneyFox.Application;
using MoneyFox.BusinessDbAccess.PaymentActions;
using MoneyFox.BusinessLogic.Adapters;
using MoneyFox.BusinessLogic.PaymentActions;
using MoneyFox.Presentation.Facades;
using MoneyFox.Persistence;

namespace MoneyFox.Uwp.Tasks
{
    /// <summary>
    ///     Background task to periodically clear payments.
    /// </summary>
    public sealed class ClearPaymentsTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            Debug.WriteLine("ClearPayment started");
            ExecutingPlatform.Current = AppPlatform.UWP;

            var settingsFacade = new SettingsFacade(new SettingsAdapter());

            try
            {
                var context = EfCoreContextFactory.Create();
                await new ClearPaymentAction(new ClearPaymentDbAccess(context))
                    .ClearPayments();
                await context.SaveChangesAsync();
            } 
            catch (Exception ex)
            {
                Debug.Write(ex);
                Debug.WriteLine("ClearPaymentTask stopped due to an error.");

            } finally
            {
                settingsFacade.LastExecutionTimeStampClearPayments = DateTime.Now;
                Debug.WriteLine("ClearPaymentTask finished.");
                deferral.Complete();
            }
        }
    }
}
