# NeuroEvoComputing
Educational implementation of neuroevolutionary ESP-algorithm for training fully connected neural network.
Project was made within course of Neuroevolutionary Computing in Tomsk Polytechnic University.
</br>
### Used packages
---
Following packages were used in project: CommandLineParser, CsvHelper, Microsoft.Extensions.DependencyInjection, Microsoft.Extensions.DependencyInjection.Abstractions, Newtonsoft.Json ScottPlot.
</br>
### Features
---
- Training fully connected neural network using ESP-algorithm
- Visualisation of training results (metrics, model architecture, best model so far and etc.)
</br>

### ESP-Algorithm
---
ESP can be used to evolve any type of neural network that consists of a single hidden
layer, such as feed-forward, simple recurrent (Elman), fully recurrent, and second-order
networks
Used algorithm can be found in [this paper](https://www.cs.utexas.edu/users/nn/downloads/papers/gomez.phdtr03.pdf).
</br>

### Set up
---
1. Pull repository to your local machine
2. Ensure that you have .NET 6 installed
3. Open Solution in IDE (Visual Studio, Rider)
4. Set up command line arguments, more info [here](https://dailydotnettips.com/how-to-pass-command-line-arguments-using-visual-studio/) (-p 3 -n 20 -f {path-to-repository}\esp\Datasets\cancer1.csv)
5. Open StartUp.cs, set additional parameters (TargetFitness, GenerationsLimit, TrainingTimeLimit)
6. Run Ga.Host project.
7. Output files will be available in {path-to-repository}\esp\src\Ga.Host\bin\Debug\net6.0\yyyy-MM-dd/HH-mm-ss
