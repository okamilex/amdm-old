$(function () {

    debugger;
    var con = $.hubConnection();
    var hub = con.createHubProxy('myHyb');
    

    // Ссылка на автоматически-сгенерированный прокси хаба
    var chat = $.connection.MyHub;

   

    chat.client.logged = function (time) {
        // Добавление сообщений на веб-страницу 
        $('#alertDiv').append('<div class="alert alert-info fade in"> '+
    '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>'+
    '<strong>Info!</strong> This alert box could indicate a neutral informative change or action.'+
  '</div>');
    };



   

   

    // Открываем соединение
    $.connection.hub.start().done(function () {
    });
});
