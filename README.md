# Issue

If you install System.Diagnostics.DiagnosticSource >= 4.7.1 corrolation is broken with servicebustriggers.

# Why it's an issue

Many libraries make use of ActivitySource and Activities for OpenTelemetry concepts.

This makes it that if you install any of those libraries you are locked in over 4.7.1 and loosing corrolation with service bus triggers.

# What you will need

A Resource group on azure with **Application Insights** and a **Service Bus Namespace**

Create a queue in the service bus namespace and call it **message-queue**

# How to test

You have to configure ```ServiceBusConnection``` and ```APPINSIGHTS_INSTRUMENTATIONKEY``` in both of the projects.

Since the are secrets I use **Managed Secrets** in the **API** file and ```local.settings.json``` in the **FunctionApp**.

Run a multiple startup project with both projects started.


Visit https://localhost/swagger running with kestrel on the API.

It should be obvious from there on :)

# Related GitHub issue

https://github.com/Azure/azure-functions-durable-extension/issues/1615 

# Images

## Not Broken

> Just remoove System.Diagnostics.DiagnosticSources or install <= 4.6.0 

![Not Broken](/not-broken.PNG)

## Broken

> Not linking the two operation id's

![Broken1](/broken-1.PNG)
![Broken2](/broken-2.PNG)
