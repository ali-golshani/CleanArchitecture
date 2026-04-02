namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder.Activities;

internal readonly record struct ApproveRequest(Request Request, int TryNumber);
