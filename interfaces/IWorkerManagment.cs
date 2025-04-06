using System;
namespace Pizzeria.interfaces;

public interface IWorkerManagment
{
    //zwalnianie pracownika
    protected void FireWorker();
    //zatrudnianie pracownika
    protected void HireWorker();
    //zwiękasznie pensji pracownika
    protected void IncreseSalary();
}
