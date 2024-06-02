# JCS
J-Category Systems: cross-platform type sharing, functional programming and immutable architecture

## Study

### RPC as a Functor: Cross-Platform Type Sharing

#### Abstract
The categories of data types are used in this paper to study programming languages in cross-platform distributed systems. We introduce the concept of functor to better understand the remote procedure call (RPC). When many programming languages are involved, type sharing plays an essential role in enabling the RPC as a functor. We also discussed a method to automatically implement the type sharing in the coding level.

#### Introduction

This paper proposes a systematic approach to designing cross-platform distributed systems. The concept \textbf{cross-platform} stands for applications(programs) developed in different programming languages. In this case, one of the most fundamental challenges is the inter-operation of different data \textbf{type systems} supported by different programming languages.\newline

For decades, \textbf{Remote Procedure Call (RPC)} has been a standard approach for the implementation of distributed systems. Typically a client calls remote functions (procedures) on a server and expects a result like running on the local host. Today Rest API is more popular, while the idea is similar. What we discuss in this paper also applies to the Rest API approach. The \textbf{procedure} in RPC is the concept we will study thoroughly. We use the mathematically strict term (pure) \textbf{function} instead.\newline

RPC plays an important role in distributed systems. However, the design of a RPC system is not easy, since it is intrinsically cross-platform. Usually in this situation, a protocol is required. When designing a protocol, one used to weaken the concept of type to accommodate heterogeneous environments. However, we advocate strong-\textbf{typed protocol} and let type itself play a central role in the design of a protocol.\newline

The main contributions of this paper include:

- Introduce the concept of functor to understand the RPC approach
- Provide a category for type sharing among different programming languages
- Propose a method to automatically implement the type sharing in the coding level
- Hence let the RPC as a functor works without the heavy burden of human coding

This paper discusses type systems intensively using \textbf{category theory}. For basic concepts of category theory, a few references are provided. \cite{MacLane1998} is the standard textbook for mathematicians but programmers. \cite{Riehl2016} is a modern textbook. For computer scientists interested in category theory, \cite{Fong2019} is recommended. \cite{Fong2020} and \cite{Ahrens2022} are also good introductory guides.\newline

We advocate strong-typed functional programming languages that have been equipped with much better type induction and error-correction capabilities. \cite{Borsatti2022} gives the conceptual relationship between category theory and functional programming.\newline

\cite{Spivak2012} and \cite{Spivak2014} presents a good example of data migration between relational data schema, using the principles of category theory. Data migration is essentially about type conversion. In this paper, we focus on type systems in general.


[Zenodo 11397406](https://doi.org/10.5281/zenodo.11397406)


