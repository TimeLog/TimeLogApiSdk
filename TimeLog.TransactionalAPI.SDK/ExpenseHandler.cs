﻿using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TimeLog.TransactionalAPI.SDK.ExpenseService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.TransactionalAPI.SDK;

/// <summary>
///     Handler of functionality related to the expense service
/// </summary>
public class ExpenseHandler : IDisposable
{
    private static ExpenseHandler? _instance;

    private bool _collectRawRequestResponse;
    private ExpenseServiceClient? _expenseClient;

    /// <summary>
    ///     Prevents a default instance of the <see cref="ExpenseHandler" /> class from being created.
    /// </summary>
    private ExpenseHandler()
    {
        _collectRawRequestResponse = false;
    }

    /// <summary>
    ///     Gets the singleton instance of the <see cref="ExpenseHandler" />.
    /// </summary>
    public static ExpenseHandler Instance => _instance ??= new ExpenseHandler();

    /// <summary>
    ///     Gets the uri associated with the expense service.
    /// </summary>
    public string ExpenseServiceUrl
    {
        get
        {
            if (SettingsHandler.Instance.Url.Contains("https"))
            {
                return SettingsHandler.Instance.Url + "WebServices/Expenses/V1_0/ExpensesServiceSecure.svc";
            }

            return SettingsHandler.Instance.Url + "WebServices/Expenses/V1_0/ExpensesService.svc";
        }
    }

    /// <summary>
    ///     Gets the expense token for use in other methods. Makes use of SecurityHandler.Instance.Token.
    /// </summary>
    public SecurityToken Token =>
        new()
        {
            Expires = SecurityHandler.Instance.Token.Expires,
            Hash = SecurityHandler.Instance.Token.Hash,
            Initials = SecurityHandler.Instance.Token.Initials
        };

    /// <summary>
    ///     Gets or sets a value indicating whether all raw XML requests should be stored in memory to allow saving them
    /// </summary>
    public bool CollectRawRequestResponse
    {
        get => _collectRawRequestResponse;

        set
        {
            _collectRawRequestResponse = value;
            _expenseClient = null;
        }
    }

    /// <summary>
    ///     Gets the active expense client
    /// </summary>
    public ExpenseServiceClient ExpenseClient
    {
        get
        {
            if (_expenseClient == null)
            {
                var endpoint = new EndpointAddress(ExpenseServiceUrl);
                if (CollectRawRequestResponse)
                {
                    var binding = new CustomBinding();
                    var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                    binding.Elements.Add(encoding);
                    binding.Elements.Add(ExpenseServiceUrl.Contains("https")
                        ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                        : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                    _expenseClient = new ExpenseServiceClient(binding, endpoint);
                }
                else
                {
                    var binding = new BasicHttpBinding
                    {
                        MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                    };

                    if (ExpenseServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    _expenseClient = new ExpenseServiceClient(binding, endpoint);
                }

                _expenseClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
            }

            return _expenseClient;
        }
    }

    void IDisposable.Dispose()
    {
        _expenseClient = null;
        _instance = null;
    }
}