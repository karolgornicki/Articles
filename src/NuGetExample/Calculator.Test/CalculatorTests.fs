namespace Calculator.Tests 

open Xunit

type CalculatorTests() = 

    [<Fact>]
    let add_1and2_returns3 () = 
        // Act 
        let result : int = CalculatorFunctions.add 1 2 

        // Assert
        Assert.Equal( 3, result )
