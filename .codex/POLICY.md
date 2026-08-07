# JCS project-local policy operation

JCS uses project-local policy-as-code. Repository facts live in `policy.json`,
and the hook implementation lives under `.codex/hooks`. Nothing in this policy
changes the global Codex permission profile or governs paths outside JCS.

The initial mode is `audit`: findings and missing verification are reported but
never rejected. After audit tests pass, set `mode` to `enforce` to block only
JCS-local destructive Git operations, recursive deletion, forceful process
termination, direct generated-file edits, and direct TypeSys execution.
Absolute operations outside `D:\DEV\JCS` are explicitly out of scope.

TypeSys itself has no default target. `scripts\regenerate-types.ps1 -Execute`
is the only supported JCS generation entry point. It validates the explicit JCS
target, resolved paths, embedded-credential prohibition, authoritative Design
change, and clean generated outputs.

Audit and session state are written under `%TEMP%\codex-policy-audit` and
`%TEMP%\codex-policy-state`. Audit records contain rule names and SHA-256 input
hashes, not raw commands or credentials.

After any hook implementation or definition changes, open a fresh task rooted
at `D:\DEV\JCS` so Codex reloads the project-local instruction chain. The
machine-wide permission remains whatever is selected in the user config.
