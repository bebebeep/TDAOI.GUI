# Signal — phase 1: core OBS control

A backend that bridges obs-websocket to a web dashboard, so you can run/stop your
stream, switch scenes, toggle sources, and control audio from any device on your
Tailscale network. This is phase 1 of the bigger plan — just the "Core" feature set.
Multi-camera ingest, IRL disconnect protection, and overlays come in later phases.

## What's here

```
cloud-obs/
  backend/        Node service: connects to obs-websocket, exposes a REST API + WebSocket feed
  dashboard/       Single-file responsive web UI (no build step)
  docker-compose.yml
```

## 1. Enable obs-websocket in OBS

OBS 28+ ships with obs-websocket built in.

1. In OBS: Tools → obs-websocket Settings
2. Check "Enable WebSocket server"
3. Set a server password — copy it, you'll need it below
4. Note the port (default 4455)

## 2. Configure the backend

```bash
cd cloud-obs/backend
cp .env.example .env
```

Edit `.env` and set `OBS_WEBSOCKET_PASSWORD` to the password from step 1.
Leave `OBS_WEBSOCKET_URL` as `ws://127.0.0.1:4455` — the container uses host
networking, so `127.0.0.1` reaches OBS on the same Debian box directly.

## 3. Run it

Through CasaOS, add this as a custom app from a Compose file, pointing at
`cloud-obs/docker-compose.yml`. Or from the command line on the server:

```bash
cd cloud-obs
docker compose up -d --build
```

## 4. Open the dashboard

From any device on your Tailscale network:

```
http://<tailscale-ip-of-debian-box>:8080
```

You should see scenes, sources for the active scene, audio inputs, and the
quick-action buttons (stream, record, studio mode, virtual cam, replay buffer,
screenshot). The top bar shows OBS connection status, live/recording state,
and bitrate/dropped-frame stats refreshed every couple of seconds.

## Notes and known limitations (intentional, for phase 1)

- **Audio input filtering**: the dashboard only lists inputs whose OBS "kind"
  is a known audio-capture type (WASAPI, PulseAudio, CoreAudio, ALSA). If an
  input doesn't show up, its `inputKind` probably isn't in that list — see
  `AUDIO_INPUT_KINDS` in `backend/server.js`.
- **No auth on the dashboard itself.** Access control right now is "you have
  to be on the Tailscale network." That's reasonable for personal use; if you
  ever expose this beyond your tailnet, put a real auth layer in front of it.
- **PiP/split-screen layouts, multi-camera ingest, auto-BRB, and overlays**
  aren't in this phase — those need MediaMTX for ingest and a small automation
  loop in the backend, which is the natural next step.

## Useful obs-websocket reference

The backend wraps these obs-websocket v5 requests — handy if you want to add
more endpoints yourself: `GetSceneList`, `SetCurrentProgramScene`,
`GetSceneItemList`, `SetSceneItemEnabled`, `GetInputList`, `SetInputMute`,
`SetInputVolume`, `StartStream`/`StopStream`, `StartRecord`/`StopRecord`,
`StartVirtualCam`/`StopVirtualCam`, `StartReplayBuffer`/`StopReplayBuffer`/
`SaveReplayBuffer`, `GetSourceScreenshot`, `SetStudioModeEnabled`,
`TriggerStudioModeTransition`.
