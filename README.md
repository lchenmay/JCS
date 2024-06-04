# JCS

This project, J-Category Systems (JCS), is the implementation of cross-platform type sharing, functional programming and immutable architecture.


## Study

### RPC as a Functor: Cross-Platform Type Sharing

We propose a systematic approach to designing cross-platform distributed systems.
The concept **cross-platform** stands for applications(programs) developed in different programming languages.
In this case, one of the most fundamental challenges is the inter-operation of different data **type systems** supported by different programming languages.

For decades, **Remote Procedure Call (RPC)** has been a standard approach for the implementation of distributed systems.
Typically a client calls remote functions (procedures) on a server and expects a result like running on the local host.
Today Rest API is more popular, while the idea is similar.
What we discuss in this paper also applies to the Rest API approach.
The **procedure** in RPC is the concept we will study thoroughly.
We use the mathematically strict term (pure) **function** instead.

RPC plays an important role in distributed systems. 
However, the design of a RPC system is not easy, since it is intrinsically cross-platform. 
Usually in this situation, a protocol is required. 
When designing a protocol, one used to weaken the concept of type to accommodate heterogeneous environments. 
However, we advocate strong-**typed protocol** and let type itself play a central role in the design of a protocol.


The main contributions of this paper include:

- Introduce the concept of functor to understand the RPC approach
- Provide a category for type sharing among different programming languages
- Propose a method to automatically implement the type sharing in the coding level
- Hence let the RPC as a functor works without the heavy burden of human coding

We discusses type systems intensively using **category theory**.
We advocate strong-typed functional programming languages that have been equipped with much better type induction and error-correction capabilities.

- Full-text of the paper: [Zenodo 11397406](https://doi.org/10.5281/zenodo.11397406)
- Cite DOI: 10.5281/zenodo.11397406


