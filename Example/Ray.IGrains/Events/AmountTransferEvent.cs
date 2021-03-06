﻿using ProtoBuf;
using Ray.Core.EventSourcing;
using Ray.Core.Lib;
using Ray.IGrains.States;
using System;

namespace Ray.IGrains.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class AmountTransferEvent : IEventBase<string>
    {
        #region base
        public string Id { get; set; }
        public uint Version { get; set; }
        public string CommandId { get; set; }
        public DateTime Timestamp { get; set; }
        public string StateId { get; set; }

        public string TypeCode => this.GetType().FullName;
        #endregion
        public string ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public AmountTransferEvent(string toAccountId, decimal amount, decimal balance)
        {
            Id = OGuid.GenerateNewId().ToString();
            ToAccountId = toAccountId;
            Amount = amount;
            Balance = balance;
        }
        public AmountTransferEvent() { }
        public void Apply(IState<string> state)
        {
            if (state is AccountState model)
            {
                this.ApplyBase(state);
                model.Balance = Balance;
            }
        }
    }
}
