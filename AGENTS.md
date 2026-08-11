# JCS project-local development contract

These instructions apply only inside the `D:\DEV\JCS` Git root. They do not
change the user-level Codex configuration or permission profile, and they do
not authorize writes to any sibling repository.

## Operating contract

- Before editing, inspect the JCS worktree and preserve every pre-existing
  change as user-owned work.
- Treat `.codex/policy.json` as the single source of truth for generated paths,
  authoritative inputs, protected operations, risk levels, dependencies, and
  verification commands. Do not duplicate those lists here.
- Use the trusted generator declared by the policy. Never hand-edit a declared
  generated path or run a direct generator entry point.
- Treat declared dependency repositories as read-only context. Writing to one
  requires a separate, explicit user request and a fresh worktree check there.
- Never write outside this Git root from a JCS task. Cross-repository work must
  use a separate task whose runtime explicitly adds that repository as a
  writable root; repository notes are not authorization.
- Do not delete or move tracked files directly. Use a separately reviewed
  controlled deletion workflow.
- Preserve every path that was already modified at session start. Do not use
  reset, restore, checkout, clean, stash removal, or an equivalent operation
  to make the baseline disappear.
- Production, remote, service, database, secret-bearing, destructive, and
  process-termination actions require an explicit current user request.

## Policy behavior

- `audit` mode records and reports findings without blocking tools or task
  completion.
- At session start, report the active project scope, policy mode, inferred risk,
  and whether the runtime permission mode is outside repository control.
- `enforce` mode may block only deterministic JCS-local rules declared in the
  policy. It is not a machine-wide permission boundary.
- Run the verification entry point declared by the policy after implementation.
  The prompt hook selects the smallest applicable task rule packs and provides
  a session-aware verification command. Use that command so the receipt can be
  tied to this task. If verification is skipped or fails, say so and do not
  present it as successful evidence.
- Treat the verification receipt's confidence separately from command success.
  `structural` means builds or static checks passed; only `behavioral` means the
  declared behavior has matching evidence. `partial` or `unknown` is not
  complete behavioral evidence.
- Treat task status `U` as an unknown-intent or unknown-failure state. Run at
  most one hypothesis-driven focused diagnostic, then report the evidence gap
  or request direction instead of repeating the broad verifier.
- Do not repeat the same broad verification after an unchanged failure. Diagnose
  the failure signature, use a focused check, or request user direction.
- Final handoff must list changed files, policy/risk decisions, verification
  evidence, skipped checks, unresolved risks, and external systems modified.
