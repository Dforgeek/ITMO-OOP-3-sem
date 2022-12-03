using Banks.BankServices;
using Banks.Console.Tools;
using Banks.TimeManagement;

var cb = new CentralBank(new ClockSimulation(DateTime.MinValue));

var mainHandler = new MainChain(cb);
mainHandler.Handle();