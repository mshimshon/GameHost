[![Version](https://img.shields.io/nuget/v/GameHost.Keys)](https://www.nuget.org/packages/GameHost.Keys)
[![](https://img.shields.io/nuget/dt/GameHost.Keys?label=Downloads)](https://www.nuget.org/packages/GameHost.Keys)
[![Build](https://github.com/mshimshon/GameHost/actions/workflows/build.yml/badge.svg)](https://github.com/mshimshon/GameHost/actions/workflows/build.yml)
[![Deploy](https://github.com/mshimshon/GameHost/actions/workflows/deploy.yml/badge.svg)](https://github.com/mshimshon/GameHost/actions/workflows/deploy.yml)

# GameHost for LunaticPanel

Deploy game servers in seconds. Not hours.

GameHost is game server plugin for [Lunatic Panel](https://github.com/mshimshon/LunaticPanel) designed for communities, hosting providers, and administrators who are tired of clunkyness of traditional panel and hours of scripting server management tasks.

Spin up a new server with a few clicks. Install Workshop content automatically. Manage mods through a modern interface. Push updates without touching SteamCMD. Control startup parameters without remembering launch arguments. Keep your communities online while GameHost handles the busywork.

Whether you're managing a small gaming community or hundreds of server instances, GameHost provides a consistent management experience across supported games.

## Why GameHost?

Because nobody starts a gaming community to spend their weekends editing configuration files.

Traditional game server management often looks like this:

- Install server manually
- Configure SteamCMD
- Download Workshop content
- Track game updates
- Restart servers
- Manage startup parameters
- Resolve mod dependencies
- Repeat

GameHost turns that into:

- Enter Deploy Command
- Select Game to Host
- Click Install
- Change Config Values
- Make a modlist

That's it.

---

## Built For Communities

GameHost was designed around two simple question:

> How do community admins expect to manage their servers?

> Why hosting companies fall short to provide the expected experience?

Modern communities expect fast deployments, reliable updates, easy configuration management and discord like experience.

---

## Features You'll Actually Use

### 🚀 One-Click Deployments

Create fully configured game server instances in minutes.

No complex command installation guides.
No manual SteamCMD workflows.

### 🔄 Automatic Game Updates

GameHost continuously monitors for updates and keeps server deployments current.

### 📦 Steam Workshop Integration

Automatically download and maintain Workshop content directly from Steam.

### 🧩 Advanced Mod Management

Inspired by Arma 3.

Create clean mod collections even for games that were never designed with proper mod list support or automatic workshop download features.

### ⚙️ Startup Parameter Editor

Manage launch arguments through a clean interface instead of memorizing paramters use strict UI guided paramters.

### 📝 Configuration Management

Edit configuration files safely through a modern UI designed to reduce mistakes.

### 🔒 Host Controlled Restrictions

Need to prevent users from changing critical settings?

By simply creating few config file at the OS level you can lock startup parameters, configuration values, or specific options while still giving users control over the rest of their server.

---

## Planned Features

### 🌐 GameHost Cluster

Synchronize servers across multiple nodes.

The goal is to allow communities and hosting providers to automatically synchronize configurations, startup parameters, mod lists, deployment profiles, and other GameHost-managed resources across multiple instances of the plugin.

Current ideas include:

- Master/Worker synchronization
- Quorum-based synchronization
- Automatic conflict resolution
- Selective synchronization policies

This feature is currently in the planning stage and not yet available.

---

### 🤖 Discord Management Bot

Discord is where communities live, so it should be where communities manage their servers.

The long-term goal is to integrate GameHost with Discord, allowing administrators to perform common management tasks without ever opening the panel.

Potential functionality includes:

- Start, stop, and restart servers
- Trigger game updates
- Switch active mod lists
- View server status and player counts
- Receive server alerts and notifications
- Execute administrative actions directly from Discord
- configurable hook routing eg: command XXX -> send RCON to game instance ALERT, Kick player, BAN whatever is available...

But server controls are only part of the vision.

GameHost aims to leverage Discord's role system as a centralized permission provider, allowing communities to manage access through Discord by assigning role to permission groups inside the panel.

Examples include:

- Assign a Discord role to grant administrator access
- Remove a Discord role to immediately revoke access
- Map Discord roles to specific GameHost permissions
- Synchronize access control automatically across communities

In addition, Discord OAuth integration is planned, allowing administrators to sign in directly with their Discord account for a seamless and familiar experience.

The objective is simple:

> Manage your servers where your community already lives.

This feature is currently planned and not yet available.

---

### 🧩 Automatic Workshop Dependency Resolution

Stop manually sorting mod dependencies.

The goal is for GameHost to analyze Steam Workshop items, extract dependency information whenever available, automatically download required dependencies, and build an appropriate load order.

For heavily modded servers this could eliminate one of the most frustrating and time-consuming administration tasks.

This feature is currently in the planning stage and not yet available.

---

### 📢 Steam Discussion Bumper

Hopefully, communities will be able to configure GameHost to automatically bump Steam discussion threads at configurable intervals.

The objective is to create a true set-and-forget experience for community recruitment and server visibility without requiring administrators to manually maintain discussion activity.

This feature is currently under evaluation.

---

### 📣 Automatic Server & Community Advertisement

Communities deserve visibility.

GameHost will eventually allow administrators to define:

- Community Description
- Server Description
- Supported Languages
- Supported Games
- Community Type
- Recruitment Preferences

Using this information, participating communities may be listed within a future GameHost Cloud Registry designed to help players discover active servers and communities.

The long-term goal is to make recruitment easier for everyone involved.

This feature is currently under consideration.

---

## Our Approach

The GameHost project is heavily community driven.

Rather than building features based purely on assumptions, we actively listen to server owners, community managers, infrastructure administrators, and hosting providers to understand real operational challenges.

Many features originate directly from frustrations encountered while running game servers at scale.

Our philosophy is simple:

> If something can be automated, automate it.
>
> If something can be simplified, simplify it.
>
> If something is frustrating, fix it.

We believe administrators should spend less time managing infrastructure and more time building great gaming communities.

---

## Our Business Model

### 🏠 Free for Individual Use

Hosting a server on your own machine for a few friends?

Charging you for that feels greedy.

GameHost is free to use for personal projects and small groups of friends.

The current target is up to 4 active players before community licensing becomes applicable.

Should your community grow beyond that point, the plugin will notify you that a Community License may soon be required.

To be clear:

GameHost will **never**:

- Shut down your server
- Prevent server startup
- Block game updates
- Interrupt active communities

We do not believe in holding player communities hostage.

Instead, access to certain advanced management functionality may become restricted until licensing requirements are met.

This may include:

- File Editor
- Startup Parameter Editor
- Mod List Manager
- Other advanced administration features

The goal is to encourage fair licensing while protecting communities and their players from unnecessary disruptions.

---

### 🌍 Community Subscription

Community pricing is intended to remain negligible.

Pricing has not yet been finalized, but the expected range currently falls between:

**$5 and $20 per server, per year**

The intent is for licensing to be based on sustained community activity rather than temporary player spikes.

A community should not require a license because ten people joined for a single evening.

The licensing system should focus on consistent usage patterns observed over time rather than isolated peak hours.

---

### 🏢 Business Subscription

Business deployments operate under a custom licensing agreement tailored to the organization's scale and operational requirements.

This tier applies to:

- Hosting providers
- Managed service providers
- Commercial gaming communities
- Infrastructure providers
- Organizations generating revenue through the use of GameHost
- Any individual or organization monetizing hosted servers or related services

Business licensing includes a one-time onboarding fee followed by annual billing.

Pricing is negotiated through a business agreement and is designed to scale alongside your operations.

Rather than enforcing hard limits, soft limits, server caps, or restrictive licensing tiers, billing is based on actual deployment volume monitored throughout the billing period.

This approach ensures that businesses only pay for the scale at which they operate, allowing licensing costs to naturally increase or decrease with business performance.

---

## Privacy

You will be able to disable general telemetry.

This includes items such as:

- Error reporting
- Usage analytics
- Operational diagnostics
- Feature usage metrics

However, certain core metrics are required for the operation of licensing and cloud-based services.

There will be no opt-out for data specifically required to:

- Validate licenses
- Determine community licensing eligibility
- Measure server player counts
- Measure peak player activity
- Operate future cloud services

GameHost does **not** require player identities for these purposes.

Only aggregate statistics such as player counts and activity patterns are expected to be collected.

The objective is to balance community privacy with the minimum information necessary to operate licensing and platform services.

---

## Our License?

[CCL](https://github.com/mshimshon/GameHost?tab=License-1-ov-file)

---

