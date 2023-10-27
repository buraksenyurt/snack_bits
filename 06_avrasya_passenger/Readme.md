# Avrasya Client

Muhtalip Dede tarafından geliştirilen Nodejs ve Typescript menşeeli Avrasya Web Framework'ünü deniyorum. Olası notları bu dokümanda toplayabilirim.

## Projenin Oluşturulması

Tabii öncesinde sistemde Nodejs olması gerektiğini hatırlatalım. Moon'da eski bir sürüm vardı. Epeydir nodejs ile yazmıyorum tabi. [Şu adresteki](https://www.hostingadvice.com/how-to/update-node-js-latest-version/) yazıya bakıo nvm komutları ile güncelledim.

```shell
sudo apt-get update
sudo apt-get install build-essential checkinstall libssl-dev
curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.32.1/install.sh | bash

# nvm 21.1.1 içerisinde npm ile birlikte geliyor
nvm install 21.1.0

# sonrasında versiyon kontrolleri
node -v
npm -v

# Her şey yoluna girince yeni projeyi oluşturdum
mkdir src
cd src
npm init
touch index.js

# Ardından avrasya paketini ekledim
npm install avrasya

# Örneği çalıştırmak içinse
npm run start
```

![avrasya_passenger_01.png](avrasya_passenger_01.png)

Repodaki örnek kodlar Typescript ile yazılmış. Ben javascript ile denemek istedim. Aşağıdaki gibi bir hata mesajı aldım. Kıt Nodejs bilgime göre hatalı bir şey de yapmış olabilirim tabii.

![avrasya_passenger_02.png](avrasya_passenger_02.png)

Bunun üzerine package.json'a hata mesajında önerdiği üzere "type":"module" bildirimini ekledim. Bu başka bir probleme neden oldu.

![avrasya_passenger_03.png](avrasya_passenger_03.png)

Bakalım. Şimdilik burada kaldım. Konu ile ilgili bir de [issue açtım](https://github.com/muhtalipdede/avrasya/issues/1).