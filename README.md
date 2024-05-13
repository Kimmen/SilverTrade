Interveiw assignment, with some additional ideas later on.

Has benchmark to be able to compare the algorithms how the effects are when increasing Days (wich randomized prices):

| Method                  | Days  | Mean            | Error         | StdDev        | Gen0    | Gen1    | Gen2    | Allocated |
|------------------------ |------ |----------------:|--------------:|--------------:|--------:|--------:|--------:|----------:|
| NPassForwardTrade       | 100   |      4,932.1 ns |      97.66 ns |     123.51 ns |  0.0534 |       - |       - |     480 B |
| TwoPassBackForwardTrade | 100   |      1,634.4 ns |       6.46 ns |       5.40 ns |  0.4272 |       - |       - |    3576 B |
| OnePassBackwardsTrade   | 100   |        327.1 ns |       0.99 ns |       0.88 ns |  0.0029 |       - |       - |      24 B |
| NPassForwardTrade       | 500   |    102,669.0 ns |   1,609.29 ns |   1,580.54 ns |  0.2441 |       - |       - |    2080 B |
| TwoPassBackForwardTrade | 500   |      8,074.9 ns |      85.90 ns |      80.35 ns |  1.9989 |       - |       - |   16768 B |
| OnePassBackwardsTrade   | 500   |      1,690.0 ns |      11.88 ns |      10.53 ns |  0.0019 |       - |       - |      24 B |
| NPassForwardTrade       | 1000  |    358,348.7 ns |   1,967.07 ns |   1,840.00 ns |  0.4883 |       - |       - |    4080 B |
| TwoPassBackForwardTrade | 1000  |     18,704.8 ns |     217.55 ns |     203.49 ns |  4.1809 |       - |       - |   35064 B |
| OnePassBackwardsTrade   | 1000  |      3,274.6 ns |       5.50 ns |       5.15 ns |       - |       - |       - |      24 B |
| NPassForwardTrade       | 5000  |  8,872,667.6 ns | 122,762.53 ns | 114,832.15 ns |       - |       - |       - |   20086 B |
| TwoPassBackForwardTrade | 5000  |    131,192.0 ns |   1,208.23 ns |   1,130.18 ns | 43.4570 | 43.4570 | 43.4570 |  183687 B |
| OnePassBackwardsTrade   | 5000  |     15,126.1 ns |      36.61 ns |      34.24 ns |       - |       - |       - |      24 B |
| NPassForwardTrade       | 10000 | 35,122,467.2 ns | 197,338.01 ns | 164,786.18 ns |       - |       - |       - |   40107 B |
| TwoPassBackForwardTrade | 10000 |    257,632.4 ns |   3,357.08 ns |   2,975.96 ns | 76.6602 | 76.6602 | 76.6602 |  323090 B |
| OnePassBackwardsTrade   | 10000 |     30,144.2 ns |     145.21 ns |     135.83 ns |       - |       - |       - |      24 B |
