# Welcome to the EventStoreDB's "From Scratch" series   
# <code style="color : green">.NET edition</code>

Cook up your own tasty recipes by following the following examples in the "FromScratch" series:
1. .NET
2. Node.js
3. Python
4. Java

The **FromScratch** series provides practical path to learn a new framework.  Follow this thorough set of instructions and example code and successsfully complete your initial project. 

# Goals of the project 

The Event Store Academy team (link to Academy when ready) wrote these with the following design goals.

1. Solve the "Doesn't run on my machine" problem by configuring and verifying success in GitHub codespaces
2. Clearly provide and explain all dependencies
3. Include instructions to configure locally, including setting up a development environment

The From Scracth cotent intends to provide EVERYTHING you need to get started in ONE place. 

# Using this repo

### 1. Start with Codespaces

A fast path to successfully running this code is to utilize GitHub codespaces.
 
For more info on codespaces please visit https://github.com/features/codespaces.

At the time of writing this all GitHub users receive 60 hours/month of free access to codespaces for non-commercial use.  As such, all you need to get started is a GitHub account.

Instructions for Running this code in Codespaces is available as a pdf here [Instructions For Running in Codespaces](./InstructionsForRunningInCodeSpaces.pdf)

### 2. Cloning this Repo and Running Locally on your computer

The steps needed to take this repository as is, and run locally are included as a pdf here. 
[Instructions For Running Locally](./InstructionsForRunningLocally.pdf)

The main difference between running in Codespaces, and running locally is that you will need to install Docker so that you can run the EventStoreDB docker containt. And you will also need to install either a JDK, a .NET sdk, python, or node.JS depending on which of the "From Scratch" projects you are running. Please see the document for details.


### 3. Setting up a local environment

This document describes the steps we took to build these examples including
* installing the programming language
* creating a directory
* setting up whatever additional tools are needed 

Also included are the steps you would want to take to prepare your code for sharing on github, either publically, or privately within your organization. 

These steps include:
* initializing a directory for git
* adding a .gitignore file
* pushing your first commit

Those steps are in this document [Setting up a local environment From Scratch](./SettingUpALocalEnvironment.pdf)


# Next Steps and Other Resources

Upon successful completion of your "From Scratch" project, you can continue practicing with the examples located at https://github.com/EventStore/samples.

In particular, the [Quickstart examples](https://GitHub.com/EventStore/samples/tree/main/Quickstart) contain more thorough examples, and include content referencing Go, Spring Boot, and Rust.




# Supporting Documents
 Included in the top level directory are the required resources:
 1. A PDF [Instructions For Running in CodeSpaces](InstructionsForRunningInCodeSpaces.pdf) outlining the steps needed to launch codespaces.
 2. A PDF [Instructions For Running Locally](InstructionsForRunningLocally.pdf) describing how to run EventStoreDB locally, including step by step details on how we built the project.
 3. A PDF [Setting Up A Local Environment](SettingUpALocalEnvironment.pdf) describes the steps taken to build this project, that can be used as a template to get started on similar projects.


# Most of all have fun!  Once you know how to write and read events "From Scratch," you will be ready to cook up all sorts of tasty and more advanced recipes. 


