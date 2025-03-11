* All pull requests must receive at least one approval by a [CODEOWNER](../CODEOWNERS) other than the author. This is enforced by GitHub itself.
* All pull requests should receive at least two approvals by [Trusted Contributors](https://github.com/letsencrypt/cp-cps/blob/main/CP-CPS.md#161-definitions).
  This requirement may be waived when:
  * the change only modifies documentation;
  * the change only modifies tests;
  * in exceptional circumstances, such as when no second reviewer is available at all.

# Deployability

We want to ensure that a new project revision can be deployed to the
currently running Boulder production instance without requiring config
changes first. We also want to ensure that during a deploy, services can be
restarted in any order. That means two things:
