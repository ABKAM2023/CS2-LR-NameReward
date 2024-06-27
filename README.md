# EN
**[C#] [LR] Module NameReward** - a plugin that gives players an experience multiplier for having a website in their nickname.

# Installation
1. Install [C# Levels Ranks Core](https://github.com/ABKAM2023/CS2-LevelsRanks-Core/tree/v1.0)
2. Download [C#] [LR] Module - Tag
3. Unpack the archive and upload it to your game server
4. Configure settings_namereward.json
5. Restart the server

# Configuration file (settings_namereward.json)
```json
{
  "Website": "example.com",
  "ExperienceMultiplier": 1.5,
  "SendRoundStartMessages": true
}
```

# Plugin translation configuration file (en.json)
```
{
    "message.pattern.match": "{DarkRed}[LR] {White}You have added {Green}{0} {White}to your nickname, and your experience will be multiplied by {Yellow}{1}{White}.",
    "message.pattern.no_match": "{DarkRed}[LR] {White}If you add {Green}{0} {White}to your nickname, your experience will be multiplied by {Yellow}{1}{White}."
}
```

# RU
**[C#] [LR] Module NameReward** - плагин, который даёт игроку множитель опыта за то, что в нике прописан сайт.

# Установка
1. Установите [C# Levels Ranks Core](https://github.com/ABKAM2023/CS2-LevelsRanks-Core/tree/v1.0)
2. Скачайте [C#] [LR] Module - Tag
3. Распакуйте архив и загрузите его на игровой сервер
4. Настройте settings_namereward.json
5. Перезапустите сервер

# Конфигурационный файл (settings_namereward.json)
```
{
  "Website": "example.com",
  "ExperienceMultiplier": 1.5,
  "SendRoundStartMessages": true
}
```

# Конфигурационный файл с переводами плагина (ru.json)
```
{
    "message.pattern.match": "{DarkRed}[LR] {White}Вы поставили в ник {Green}{0}, {White}и ваш опыт будет умножен на {Yellow}{1}{White}.",
    "message.pattern.no_match": "{DarkRed}[LR] {White}Если вы добавите {Green}{0} {White}в ник, ваш опыт будет умножен на {Yellow}{1}{White}."
}
```
