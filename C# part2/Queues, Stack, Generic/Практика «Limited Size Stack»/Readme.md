<div class="slide">
                <h1>
                    <span class="slide__title">
Практика «Limited Size Stack»                    </span>
                    <span class="score">10 баллов из 10</span>
                </h1>
				<p>В этой задаче вам нужно реализовать стек ограниченного размера.
Этот стек работает как обычный стек, однако при превышении максимального размера удаляет самый глубокий элемент в стеке.
Таким образом в стеке всегда будет ограниченное число элементов.</p>
<p>Вот пример работы такого стека с ограничением в 2 элемента:</p>
сначала стек пуст
stack.Push(10); // в стеке 10
stack.Push(20); // в стеке 10, 20
stack.Push(30); // в стеке 20, 30
stack.Push(40); // в стеке 30, 40
stack.Pop(); // возвращает 40, в стеке остаётся 30
stack.Pop(); // возвращает 30, стек после этого пуст
<p>Операция Push должна иметь сложность O(1), то есть никак не зависеть от размера стека.</p>
<p><a href="/Exercise/StudentZip?courseId=BasicProgramming2&amp;slideId=cdf76069-758c-4a3c-aacb-df3fa877cac5">Скачайте проект LimitedSizeStack</a>. Реализуйте класс <code>LimitedSizeStack</code>.</p>
<p>Отладьте его реализацию с помощью тестов в классе <code>LimitedSizeStack_should</code>. 
Проверьте эффективность операции Push с помощью теста из класса <code>LimitedSizeStack_PerformanceTest</code>.</p>

https://ulearn.me/Course/BasicProgramming2/Praktika_Limited_Size_Stack__cdf76069-758c-4a3c-aacb-df3fa877cac5
