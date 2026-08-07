# JCS project-local policy

This directory is a repository-local control plane. It does not install or
modify `C:\Users\Aiden\.codex`, Windows permissions, the Codex permission
profile, or another repository.

## Architecture

`policy.json` is the single machine-readable source for repository scope,
authoritative and generated paths, trusted generators, deterministic rules,
risk levels, the dependency graph, verification checks, and the receipt path.
`AGENTS.md`, hooks, and verification scripts consume that declaration instead
of maintaining independent copies.

Risk levels are intentionally asymmetric:

- L1: ordinary local changes; focused checks.
- L2: schema, generated code, shared APIs, policy, or dependency-sensitive work.
- L3: destructive, secret, production, database, service, or remote actions;
  the action requires explicit authorization.

`audit` is the safe development default. It records findings but never denies a
tool call or blocks completion. `enforce` blocks only deterministic rules for
events that resolve inside the JCS root. An explicit absolute target outside
JCS is ignored by this policy rather than becoming globally governed.

The Codex runtime permission profile is deliberately outside this repository's
control. `runtimePermissions.managedByRepository` must remain `false`; changing
the project policy can never grant machine-wide, server, or sibling-repository
access.

## Lifecycle

Use `scripts\codex-policy-mode.ps1` to change modes and then start a new Codex
task rooted at JCS. Codex hashes non-managed Hook definitions, so review and
trust changed project Hooks again with `/hooks` before expecting them to run.

Use `scripts\codex-policy-review.ps1` for a read-only preview of scope, inferred
risk, dependencies, and planned checks. Use `scripts\codex-verify.ps1` for
verification. It derives the risk level and
check list from `policy.json`, then writes a concise machine-readable receipt to
the declared ignored output path. The verifier performs local checks only; the
dependency graph is declared for impact analysis and never grants sibling
repository writes.

`scripts\codex-dependency-impact.ps1` inspects only dependency existence, Git
revision, dirty-status count, and impact patterns. It does not build, edit, or
clean Common or Aiarwa, and it never treats that inspection as compatibility
proof.

All policy-declared `dotnet build` checks must include `--no-dependencies`.
This makes a missing prebuilt Common dependency a visible prerequisite instead
of silently compiling into the sibling repository. BizLogics is selected only
when its own code or shared/type-generation inputs change, or when `-Full` is
explicitly requested.

Verification receipts classify failures as `tooling-prerequisite`,
`compile-or-typecheck`, or `command-failure`. Use the optional repository-local
`-ReceiptPathOverride` when a full audit receipt must be preserved separately;
outside-repository receipt targets are rejected.

Checks that can generate files declare deterministic preconditions. Frontend
checks verify the local `vue-tsc` installation before invoking `npm run test`,
so a missing dependency cannot dirty generated route files before failing.

Audit and session state are stored below `%TEMP%\codex-policy-audit` and
`%TEMP%\codex-policy-state`. Records contain rule names and SHA-256 hashes, not
raw commands or credentials.
