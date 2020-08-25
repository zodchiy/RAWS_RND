# RAWS_RND
***RabbitMQ + Angular + WebSocket + SignalR + Docker compose = doesn't work yet ¯\_(ツ)_/¯ RnD Project***

- BackgroundService для загрузки данных по WebSocket в реальном времени и отдача этих данных в шину (RabbitMQ)
- lifetime.ApplicationStarted для создания подписчика на стророне web api
- ну и SignalR + Angular для отоборажения данных пользователю

На что потерял чудовищно много  времени - на уживание SpaAngular template с Docker compose, проблемы связаны с пробрасыванием портов и прокси, а также билдингом.
Зачем? Уж очень узкая тема RabbitMQ + SignalR, тем более когда данные летят в реалтайме.

Через пару дней продолжу.
