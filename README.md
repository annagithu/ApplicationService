1. перейти в папку - ApplicationService\Docker
2. из папки вызвать консоль и ввести команду docker compose up -d
3. ввести в строку поиска - http://localhost:8088/swagger/index.html

(база данных находится на порту 5532, пользователь postgres, пароль - 1)

*чтобы посмотреть логи: docker compose logs -f
*чтобы остановить проект: docker compose down
*чтобы остановить проект и удалить данные: docker compose down -v
---------------------------------------
поле isSubmitted - если false, то заявка является "черновиком". если true, то считается отправленной на рассмотрение
