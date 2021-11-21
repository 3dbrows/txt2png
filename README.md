# txt2png

A utility which accepts a string via HTTP endpoint, and returns a PNG graphic of that text.

![Docker Build](https://img.shields.io/docker/cloud/build/3dbrows/txt2png.svg)
[![Coverage Status](https://coveralls.io/repos/github/3dbrows/txt2png/badge.svg?branch=master)](https://coveralls.io/github/3dbrows/txt2png?branch=master)
[![CodeFactor](https://www.codefactor.io/repository/github/3dbrows/txt2png/badge)](https://www.codefactor.io/repository/github/3dbrows/txt2png)
![Tests](https://github.com/3dbrows/txt2png/workflows/.NET%20Core%20Tests/badge.svg)

## Use Cases
txt2png lets you generate obfuscated base64-encoded inline PNG images. This is useful in cases like these:

* Include an email address in a web page, while hiding it from (low-effort) scrapers: ![Image generated by github.com/3dbrows/txt2png](https://github.com/3dbrows/txt2png/raw/master/img/eg1.png)
* Embed politically sensitive text which is only rendered at page load time: ![Image generated by github.com/3dbrows/txt2png](https://github.com/3dbrows/txt2png/raw/master/img/eg2.png)
* Dynamically generate an image from a URI query string.

## Quick start
You can experiment with this tool at [https://txt2png.3dbrows.dev](https://txt2png.3dbrows.dev). Please don't use this hosted service for anything but trying out the tool; it's not for any production workloads. For that, host it yourself.

A Docker image is available on DockerHub at [3dbrows/txt2png](https://hub.docker.com/r/3dbrows/txt2png). Example use:
```
docker run -p "8080:80" -e Input__MaxLength=80 -e Logging__LogLevel__Microsoft=None 3dbrows/txt2png
```
The optional flag `-e Input__MaxLength=80` (N.B. **two** underscores) sets the max input length to `80` chars instead of the default `280`. The optional flag `-e Logging__LogLevel__Microsoft=None` turns off logging (also two underscores), so you can avoid keeping logs of user requests if you wish. (_"What you don't log can't be subpoenaed."_)

Access the test harness in your browser at `http://localhost:8080` or make command-line requests like so:
```
curl -i -H "Accept: text/plain" http://localhost:8080/Txt2Png/v1?input=foo%40example.com
```
In the test harness, use the "Try it out" function and toggle between `image/png` and `text/plain` to obtain the PNG image or the Base64 encoding thereof.

An OpenAPI v3 specification is available at `/swagger/v1.0/swagger.json`.

## Examples
Here is how to generate this image: ![Image generated by github.com/3dbrows/txt2png](https://github.com/3dbrows/txt2png/raw/master/img/eg1.png)

### Base64 output
```
$ curl -i -H "Accept: text/plain" http://localhost:8080/Txt2Png/v1?input=foo%40example.com&background=white
```
Use the returned Base64 string in HTML like this:
```html
<img alt="Image generated by github.com/3dbrows/txt2png" src="data:image/png;base64,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" />
```

Or, use it in Markdown like this:
```markdown
![Image generated by github.com/3dbrows/txt2png](data:image/png;base64,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)
```

### Binary output
```
$ curl -i http://localhost:8080/Txt2Png/v1?input=foo%40example.com&background=white
```
(If you want, you can optionally specify a header of `Accept: image/png`.)

Use it in HTML like this:
```html
<img alt="Image generated by github.com/3dbrows/txt2png" src="http://localhost:8080/Txt2Png/v1?input=foo%40example.com&background=white" />
```

Or, use it in Markdown like this:
```markdown
![Image generated by github.com/3dbrows/txt2png](http://localhost:8080/Txt2Png/v1?input=foo%40example.com&background=white)
```

In this example, the input text is within your page source, which offers no privacy at all; so don't use this method if you are trying to conceal anything from scrapers.

## Important notes
* Please URI-encode the input.
* Consider accessibility concerns e.g. how will a blind person's screen reader react to the image? Use meaningful alt-text in the `img` tag.
* Be advised that any attempts to obtain security or privacy through obscurity are ineffective against any sufficiently determined attacker. This tool merely raises the threshold of effort required to harvest mildly sensitive data (such as email addresses). Please do not confuse this tool for any form of cryptography.
* Please note that GitHub does not support the display of embedded Base64 images in Markdown (using `data:image/png;base64,...`).
* Style options are black text on a white or transparent background, or white text on a black background. This allows a monochromatic (hence smaller) image.

## Licencing and Contributing
Please see [LICENSE](LICENSE) and [CONTRIBUTING.md](CONTRIBUTING.md).

Donations welcome: [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Q5Q71TISU)
